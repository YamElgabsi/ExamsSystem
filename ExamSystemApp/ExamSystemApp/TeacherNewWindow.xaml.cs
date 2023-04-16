using ExamSystemApp.Models;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace ExamSystemApp

{
    /// <summary>
    /// Interaction logic for TeacherNewWindow.xaml
    /// </summary>
    public partial class TeacherNewWindow : Window
    {
        User teacher;
        Exam exam;
        bool isNew;
        HttpClient clientApi;


        public TeacherNewWindow(User user, Exam examToEdit, bool isNewExam)
        {
            InitializeComponent();

            teacher = user;
            exam = examToEdit;
            isNew = isNewExam;
            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7002");

            if (isNewExam && exam.Questions == string.Empty)
            {
                Question question = new Question();
                questionsLB.Items.Add(question);
                questionsLB.SelectedIndex = 0;
            }
            else
            {
                var questions = System.Text.Json.JsonSerializer.Deserialize<List<Question>>(exam.Questions);
                foreach (Question question in questions)
                {
                    questionsLB.Items.Add(question);
                }

                questionsLB.SelectedIndex = 0;
            }     
        }



        private void btnAddAns_Click(object sender, RoutedEventArgs e)
        {
            
            string input = Interaction.InputBox("Enter new answer:", "New Answer", "Default", 0,0);
            if (input != "")
            {
                Question question = questionsLB.SelectedItem as Question;
                question.AllA.Add(input);
                answersSP.Items.Add(input);
                answersSP.SelectedItem = input;
            }
            else
            {
                MessageBox.Show("The answer cannot be empty");
            }
           
        }

        private void btnRmvAns_Click(object sender, RoutedEventArgs e)
        {
            if(answersSP.Items.Count == 1)
            {
                MessageBox.Show("questiom can not be with 1 answer");
                return;
            }

            if(answersSP.Items[answersSP.SelectedIndex] != null ) {
                string old = answersSP.SelectedItem as string;
                answersSP.Items.RemoveAt(answersSP.SelectedIndex);
                Question question = questionsLB.SelectedItem as Question;
                question.AllA.RemoveAt(answersSP.SelectedIndex);
                answersSP.SelectedIndex = 0;
            }
        }

        private void btnEditAns_Click(object sender, RoutedEventArgs e)
        {
            
            if (answersSP.SelectedItem != null)
            {
                var idx = answersSP.SelectedIndex;
                string oldAnswer = answersSP.SelectedItem as string;

                string input = Interaction.InputBox("Enter an answer:", "New Answer", oldAnswer, 0, 0);
                if (input != "" && input != oldAnswer)
                {
                    answersSP.Items[answersSP.SelectedIndex] = input;
                    Question question = questionsLB.SelectedItem as Question;
                    question.AllA[idx] = input;
                    answersSP.SelectedIndex = idx;
                }



            }
        }

        private void quesViewImageIMG_Click(object sender, RoutedEventArgs e)
        {
            if (questionsLB.SelectedIndex != -1)
            {
                Question question = questionsLB.SelectedItem as Question; //it is always be a Question so it is OK
                string oldQuestion = question.Q;

                double x = this.Left + (this.Width );
                double y = this.Top + (this.Height );
                string input = Interaction.InputBox("Enter an Question:", "New Question", oldQuestion, (int) x, (int) y);
                if (input != "")
                {
                    question.Q = input;
                    question.IsPicture = false;
                    txtName.Text = input;
                    txtName.Background = null;
                }


            }
        }

        private void answersSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( answersSP.SelectedItem != null ) 
            {
                Question question = questionsLB.SelectedItem as Question;
                question.A = answersSP.SelectedItem as string;
            }
            
        }

        private void btnAddQ_Click_1(object sender, RoutedEventArgs e)
        {
            int numberOfQuestiom = questionsLB.Items.Count;
            Question question = new Question();
            question.Id = numberOfQuestiom + 1;
            questionsLB.Items.Add(question);
            questionsLB.SelectedIndex = numberOfQuestiom;
        }

        private void btnRmvQ_Click(object sender, RoutedEventArgs e)
        {
            if(questionsLB.Items.Count != 1)
            {
                Question removedQuestion = questionsLB.SelectedItem as Question;
                questionsLB.Items.RemoveAt(removedQuestion.Id - 1);

                for (int i = removedQuestion.Id - 1; i < questionsLB.Items.Count; i++)
                {
                    Question question = questionsLB.Items[i] as Question;
                    question.Id -= 1;
                }

                questionsLB.SelectedIndex = 0;

            }
            else
            {
                MessageBox.Show("cannot leave the exam without question");
            }

        }

        private void questionsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if(questionsLB.SelectedItem != null)
            {
                Question selectedQ = questionsLB.SelectedItem as Question;
                if(selectedQ.IsPicture)
                {
                    txtName.Text = "";
                    var bitmap = Base64ToImage(selectedQ.Q);
                    txtName.Background = new ImageBrush(bitmap);

                }
                else
                {
                    txtName.Text = selectedQ.Q;
                    txtName.Background= null;

                }
                
                answersSP.Items.Clear();
                int idxAns = 0;

                foreach (var answer in selectedQ.AllA)
                {
                    answersSP.Items.Add(answer);
                    if ( answer == selectedQ.A )
                    {
                        answersSP.SelectedIndex = idxAns;
                    }
                    idxAns++;
                }

            }
            

        }
        
     

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions= new List<Question>();
            foreach (var question in questionsLB.Items) { 
                Question q = question as Question;
                questions.Add(q);
            }
            exam.Questions = System.Text.Json.JsonSerializer.Serialize<List<Question>>(questions);
            string jsonString = System.Text.Json.JsonSerializer.Serialize<Exam>(exam);

            if (isNew)
            {
                using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage result = clientApi.PostAsync("api/Exams", content).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        MessageBox.Show($"The Exam - {exam.Name} Created!");
                        Close();
                    }
                        
                    else
                    {
                        string returnValue = result.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to POST data: ({result.StatusCode}): {returnValue}");
                    }

                }
            }
            else
            {
                using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage result = clientApi.PutAsync("api/Exams/"+exam.ExamId, content).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        MessageBox.Show($"The Exam - {exam.Name} Updated!");
                        Close();
                    }
                    else
                    {
                        string returnValue = result.Content.ReadAsStringAsync().Result;
                        MessageBox.Show($"Failed to POST data: ({result.StatusCode}): {returnValue}");
                    }

                }

            }
        }

        private void quesViewTextIMG_Click(object sender, RoutedEventArgs e)
        {
            if (questionsLB.SelectedItem != null)
            {
                Question question = questionsLB.SelectedItem as Question;
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    var image = new Image();
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFileDialog.FileName);
                    bitmap.EndInit();
                    image.Source = bitmap;
                    var base64 = ImageToBase64(bitmap);
                    question.Q = base64;
                    question.IsPicture = true;
                    txtName.Text = "";
                    txtName.Background = new ImageBrush(bitmap);
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

        private void btnSaveQS_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions = questionsLB.Items.Cast<Question>().ToList();
            string textToSave = System.Text.Json.JsonSerializer.Serialize<List<Question>>(questions);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            // Get folder path and create/save text file
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.InitialDirectory = desktopPath;
            saveFileDialog.Title = "Save text file";
            if (saveFileDialog.ShowDialog() == true)
            {
                var filePath = saveFileDialog.FileName;
                using (var streamWriter = new StreamWriter(filePath))
                {
                    // Write text to file
                    streamWriter.Write(textToSave);
                }
            }

        }

        private void btnLoasQS_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the file filter and title
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.Title = "Select a Text File";

            // Show the dialog and get the result
            var result = openFileDialog.ShowDialog();

            // If the user clicked OK
            if (result == true)
            {
                // Read the contents of the file into a string
                string fileContents = File.ReadAllText(openFileDialog.FileName);
                try
                {
                    List<Question> questions = System.Text.Json.JsonSerializer.Deserialize<List<Question>>(fileContents);
                    if (questions != null)
                    {
                        int numberOfQuestions = questionsLB.Items.Count;
                        foreach (var question in questions)
                        {
                            question.Id = ++numberOfQuestions;
                            questionsLB.Items.Add(question);

                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Not good file");
                }
                


            }
        }

        private void button_SaveExamLocal_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions = questionsLB.Items.Cast<Question>().ToList();
            exam.Questions = System.Text.Json.JsonSerializer.Serialize<List<Question>>(questions);
            string textToSave = System.Text.Json.JsonSerializer.Serialize<Exam>(exam);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            // Get folder path and create/save text file
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.InitialDirectory = desktopPath;
            saveFileDialog.Title = "Save text file";
            if (saveFileDialog.ShowDialog() == true)
            {
                var filePath = saveFileDialog.FileName;
                using (var streamWriter = new StreamWriter(filePath))
                {
                    // Write text to file
                    streamWriter.Write(textToSave);
                }
            }

        }
    }
}
