using ExamSystemApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExamSystemApp
{
    /// <summary>
    /// Interaction logic for StudentExamDetailsWindow.xaml
    /// </summary>
    public partial class StudentExamDetailsWindow : Window
    {
        User student;
        Exam exam;
        HttpClient clientApi;
        List<StudentExam>? studentExamList;

        public StudentExamDetailsWindow(User user, Exam givenExam)
        {
            InitializeComponent();

            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7002");

            this.student = user; this.exam = givenExam;
            examNameLbl.Content= exam.Name;
            teacherNameLbl.Content= exam.TeacherName;
            minutesLbl.Content= exam.Minutes;

            GetAllExams();
            
            if(studentExamList == null)
            {
                enterBtn.IsEnabled = false;
                enterBtn.Content = "Network Error";
                return;
            }
            else
            {
                foreach(var student_exam in studentExamList)
                {
                    if((student_exam.ExamId== exam.ExamId) && (student_exam.StudentId==student.Id))
                    {
                        enterBtn.IsEnabled = false;
                        enterBtn.Content = "You Already Did This Exam ";
                        return;
                    }
                }
            }


            if (!IsTimePastButNotLongerThan30Minutes(exam.Date))
            {
                // The time is past but not longer than 30 minutes ago
                enterBtn.IsEnabled = false;
                enterBtn.Content = "The Exam date is: " + exam.Date.ToString();

            }
            

        }


        public bool IsTimePastButNotLongerThan30Minutes(DateTime dateTime)
        {
            TimeSpan elapsed = DateTime.Now - dateTime;
            return elapsed > TimeSpan.Zero && elapsed <= TimeSpan.FromMinutes(30);
        }


        public async Task GetAllExams()
        {
            HttpResponseMessage response = clientApi.GetAsync("api/StudentExams").Result;
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var m = JsonConvert.DeserializeObject<List<StudentExam>>(jsonResponse);
                studentExamList = m;
            }
        }

        private void beckBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            StudentExamWindow studentExamWindow = new StudentExamWindow(student, exam);
            var result = studentExamWindow.ShowDialog();
            // If the new window was closed, close this window
            if (result == false)
            {
                Close();
            }

        }
    }
}
