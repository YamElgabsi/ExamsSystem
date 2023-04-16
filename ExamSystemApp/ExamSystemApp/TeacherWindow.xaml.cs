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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExamSystemApp
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        HttpClient clientApi;
        User _user;
        List<Exam> exams;
        List<StudentExam>? studentExams;
        public TeacherWindow(User user)
        {
            InitializeComponent();
            _user = user;
            navbarLBL.Content += " | " + user.Name;
            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7002");
            _ = GetExams();
            foreach (Exam exam in exams)
            {
                if(exam.TeacherID == _user.Id)
                {
                    examsListBox.Items.Add(exam);

                }
            }
            studentExams = null;
        }

        public async Task GetExams()
        {
            HttpResponseMessage response = clientApi.GetAsync("api/Exams").Result;
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var m = JsonConvert.DeserializeObject<List<Exam>>(jsonResponse);
                exams = m;
            }
        }

        public async Task GetStudentExams()
        {
            HttpResponseMessage response = clientApi.GetAsync("api/StudentExams").Result;
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var m = JsonConvert.DeserializeObject<List<StudentExam>>(jsonResponse);
                studentExams = m;
            }
        }

        public async Task DeleteStudentExam(string id)
        {
            HttpResponseMessage response = clientApi.DeleteAsync("api/StudentExams/" + id).Result;
        }

        

        private void SeachBTN_Click(object sender, RoutedEventArgs e)
        {
            examsListBox.Items.Clear();
            if (searchTB.Text == "")
            {
                foreach (Exam exam in exams)
                {
                    if(exam.TeacherID == _user.Id) examsListBox.Items.Add(exam);

                }
            }
            else
            {
                foreach (Exam exam in exams)
                {
                    if (exam.Name.StartsWith(searchTB.Text))
                    {
                        if (exam.TeacherID == _user.Id) examsListBox.Items.Add(exam);
                    }
                }
            }

        }

        private void addBTN_Click(object sender, RoutedEventArgs e)
        {
            TeacherNewExamDetailsWindow teacherNewExamDetailsWindow = new TeacherNewExamDetailsWindow(_user);
            var result = teacherNewExamDetailsWindow.ShowDialog();
            // If the new window was closed, close this window
            if (result == false)
            {
                _ = GetExams();
                SeachBTN_Click(sender,e);
            }
        }

        private void editBTN_Click(object sender, RoutedEventArgs e)
        {
            if(examsListBox.SelectedItem!= null)
            {
                Exam exam = examsListBox.SelectedItem as Exam;
                TeacherNewExamDetailsWindow teacherNewExamDetailsWindow = new TeacherNewExamDetailsWindow(_user, exam);
                var result = teacherNewExamDetailsWindow.ShowDialog();
                // If the new window was closed, close this window
                if (result == false)
                {
                    _ = GetExams();
                    SeachBTN_Click(sender, e);
                }
            }

        }

        private void DeleteExamBtn_Click(object sender, RoutedEventArgs e)
        {
            if (examsListBox.SelectedItem != null)
            {
                Exam exam = examsListBox.SelectedItem as Exam;

                //Delete student exams
                GetStudentExams();

                if(studentExams != null)
                {
                    foreach(StudentExam studentExam in studentExams)
                    {
                        if(studentExam.ExamId == exam.ExamId)
                        {
                            DeleteStudentExam(studentExam.Id);
                        }
                    }
                }
                

                //Delete the exam from the detabase
                var response = clientApi.DeleteAsync("api/Exams/"+exam.ExamId).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("The Exam: " + exam.Name + " Deleted!" );
                    exams.Remove(exam);
                    SeachBTN_Click(sender, e);
                }
                    

                else
                    MessageBox.Show("Error");
            }


        }

        private void StatsBTN_Click(object sender, RoutedEventArgs e)
        {
            if (examsListBox.SelectedItem != null)
            {
                Exam exam = examsListBox.SelectedItem as Exam;

                StatsWindow statsWindow = new StatsWindow(exam);
                statsWindow.ShowDialog();
            }
        }
    }
}
