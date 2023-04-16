using ExamSystemApp.Models;
using System.Text.Json;
using System.Text.Json.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Newtonsoft.Json;
using System.Security.Policy;
using Newtonsoft.Json.Linq;
using System.Windows.Threading;
using System.IO;
using System.Xml.Linq;

namespace ExamSystemApp
{

    /// <summary>
    /// Interaction logic for StudentExamWindow.xaml
    /// </summary>
    public partial class StudentExamWindow : Window
    {
        User student;
        Exam exam;
        HttpClient clientApi;
        List<ExamSubmission> submissions;
        List<bool> isAnswer;


        private readonly TimeSpan examDuration;
        private DispatcherTimer timer;
        DateTime timerStarted;


        public StudentExamWindow(User user, Exam exam)
        {
            InitializeComponent();
            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7002");

            student = user;
            this.exam= exam;

            List<Question> questions = JsonSerializer.Deserialize<List<Question>>(exam.Questions);

            isAnswer= new List<bool>();
            submissions= new List<ExamSubmission>();

            foreach( Question question in questions ) {

                if (exam.IsRandom)
                {
                    Random rand = new Random();
                    var shuffled = question.AllA.OrderBy(_ => rand.Next()).ToList();
                    question.AllA = shuffled;
                }

                string q = question.Q;
                if (question.IsPicture)
                {
                    q = "(Picture)";
                }
                submissions.Add(new ExamSubmission { CA = question.A, Q = q, SA = ""});
                isAnswer.Add(false);
                listBoxQuestions.Items.Add(question);  
            }

            ///

            examDuration = TimeSpan.FromMinutes(exam.Minutes);

            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            timerStarted = DateTime.Now;

        }

        ///ddc <summary>
        /// ddc

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeLeft = examDuration - (DateTime.Now -  timerStarted);
            if (timeLeft.TotalSeconds <= 0)
            {
                countdownText.Text = "TIME UP!";
                timer.Stop();
                submitTestNot();

            }
            else
            {
                countdownText.Text = $"Time left: {timeLeft.Hours:00}:{timeLeft.Minutes:00}:{timeLeft.Seconds:00}";
            }
        }

        private void submitTestNot()
        {
            int notAns = 0;
            int corAns = 0;
            for (int i = 0; i < submissions.Count; i++)
            {
                if (isAnswer[i])
                {
                    if (submissions[i].SA == (listBoxQuestions.Items[i] as Question).A)
                    {
                        corAns++;
                    }
                }
                else
                {
                    notAns++;
                }
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize<List<ExamSubmission>>(submissions, options);

            StudentExam studentExam = new StudentExam(exam.ExamId, student.Id, student.Name, exam.TeacherID,
                                                        100 * corAns / submissions.Count,
                                                        json);

            _ = UploadSub(studentExam);

            SolutionWindowe sol = new SolutionWindowe(studentExam);
            sol.Owner = this;
            var result = sol.ShowDialog();

            // If the new window was closed, close this window
            if (result == false)
            {
                Close();
            }


        }




        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void listBoxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBoxQuestions.SelectedItem != null)
            {
                int i = 0;
                Question question = listBoxQuestions.SelectedItem as Question;
                if (question.IsPicture)
                {
                    QuestionTB.Text = "";
                    var bitmap = Base64ToImage(question.Q);
                    QuestionTB.Background = new ImageBrush(bitmap);


                }
                else
                {
                    QuestionTB.Text = question.Q;
                    QuestionTB.Background = null;
                }

                answersLB.Items.Clear();
                foreach (string ans in question.AllA)
                {
                    answersLB.Items.Add(ans);
                    if (isAnswer[question.Id - 1] && ( ans == submissions[question.Id - 1].SA) )
                    {
                        answersLB.SelectedIndex = i;
                    }
                    i++;
                }
            }
            
       
        }

        private void answersLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(answersLB.SelectedItem != null)
            {
                submissions[listBoxQuestions.SelectedIndex].SA = answersLB.SelectedItem as string;
                isAnswer[listBoxQuestions.SelectedIndex] = true;
                
            }
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            
            int notAns = 0;
            int corAns = 0;
            for(int i = 0; i<submissions.Count; i++)
            {
                if(isAnswer[i])
                {
                    if(submissions[i].SA == (listBoxQuestions.Items[i] as Question).A)
                    {
                        corAns++;
                    }
                }
                else
                {
                    notAns++;
                }
            }

            if (notAns != 0)
            {
                MessageBox.Show($"You have {notAns.ToString()} unanswered questions!\nIf you do not know the answer make a guess");
            }
            else
            {

                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize<List<ExamSubmission>>(submissions, options);

                StudentExam studentExam = new StudentExam(exam.ExamId, student.Id, student.Name, exam.TeacherID,
                                                          100 * corAns / submissions.Count,
                                                            json);

                timer.Stop();
                _ = UploadSub(studentExam);

                SolutionWindowe sol = new SolutionWindowe(studentExam);
                sol.Owner = this;
                var result = sol.ShowDialog();

                // If the new window was closed, close this window
                if (result == false)
                {
                    Close();
                }

            }
            
        }

        async Task UploadSub(StudentExam studentExam)
        {

            string jsonString = JsonSerializer.Serialize<StudentExam>(studentExam);


            using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result = clientApi.PostAsync("api/StudentExams", content).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {

                }

                else
                {
                    string returnValue = result.Content.ReadAsStringAsync().Result;
                    MessageBox.Show($"Failed to POST data: ({result.StatusCode}): {returnValue}");
                }
                
            }
            
        }

        public static string ImageToBase64(BitmapImage bitmapImage)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Create a BitmapEncoder and add the BitmapImage to it
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

                // Save the BitmapEncoder to the memory stream
                encoder.Save(memoryStream);

                // Convert the memory stream to a byte array
                byte[] imageBytes = memoryStream.ToArray();

                // Convert the byte array to a Base64-encoded string
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
        }

        public static BitmapImage Base64ToImage(string base64String)
        {
            // Convert the Base64-encoded string to a byte array
            byte[] imageBytes = Convert.FromBase64String(base64String);

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                // Create a new BitmapImage and set its source to the memory stream
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }


    }
}


