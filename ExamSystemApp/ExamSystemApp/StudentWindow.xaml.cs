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
using ExamSystemApp.Models;
using Newtonsoft.Json;

namespace ExamSystemApp
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        HttpClient clientApi;
        User _user;
        List<Exam> exams;
        public StudentWindow(User user)
        {
            InitializeComponent();
            _user = user;
            navbarLBL.Content += " | " + user.Name;
            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7002");
            _ = GetExams();
            foreach (Exam exam in exams)
            {
                examsListBox.Items.Add(exam);
            }

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

        private void SeachBTN_Click(object sender, RoutedEventArgs e)
        {
            examsListBox.Items.Clear();
            if (searchTB.Text == "")
            {
                foreach (Exam exam in exams)
                {
                    examsListBox.Items.Add(exam);
                }
            }
            else
            {
                foreach (Exam exam in exams)
                {
                    if (exam.Name.StartsWith(searchTB.Text))
                    {
                        examsListBox.Items.Add(exam);
                    }
                }
            }  

        }

        private void EnterBTN_Click(object sender, RoutedEventArgs e)
        {
            Exam? selected = examsListBox.SelectedItem as Exam;
            if (selected != null)
            {
                StudentExamDetailsWindow studentExamDetailsWindow = new StudentExamDetailsWindow(_user,selected);
                studentExamDetailsWindow.ShowDialog();
            }
        }
    }
}
