using ExamSystemApp.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        Exam exam;
        HttpClient clientApi;
        public List<ExamSubmission> ExamSubmissions { get; set; }

        List<StudentExam> studentExams;
        List<StudentExam>? allStudentExams;
        double avg = 0;
        int highest = 0;
        int lowest = 100;



        public StatsWindow(Exam selected_exam)
        {
            InitializeComponent();

            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7002");

            exam = selected_exam;
            examNameLBL.Content = exam.Name;

            GetAllExams();

            if (allStudentExams == null) return;

            studentExams = new List<StudentExam>();
            foreach (var studentExam in allStudentExams)
            {
                if (studentExam.ExamId == exam.ExamId)
                {
                    studentExams.Add(studentExam);
                    int grade = studentExam.Grade;
                    if (grade > highest) highest = grade;
                    if (grade < lowest) lowest = grade;
                    avg += grade;
                }
            }
            label_sum.Content = studentExams.Count.ToString();

            if (studentExams.Count != 0) {
                label_avg.Content = ((int)avg/ studentExams.Count).ToString();
                label_high.Content = highest.ToString();
                label_low.Content = lowest.ToString();

                foreach(var studentExam in studentExams)
                {
                    listBox_StudentsExams.Items.Add(studentExam);
                }

            }

            ExamSubmissions = new List<ExamSubmission>();
            DataContext = this;







        }

        public async Task GetAllExams()
        {
            HttpResponseMessage response = clientApi.GetAsync("api/StudentExams").Result;
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var m = JsonConvert.DeserializeObject<List<StudentExam>>(jsonResponse);
                allStudentExams = m;
            }
        }

        private void listBox_StudentsExams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox_StudentsExams.SelectedItem != null)
            {
                StudentExam student_exam = listBox_StudentsExams.SelectedItem as StudentExam;
                label_id.Content = student_exam.StudentId;
                label_grade.Content = student_exam.Grade.ToString();
                label_name.Content = student_exam.StudentName.ToString();
                ExamSubmissions = JsonConvert.DeserializeObject<List<ExamSubmission>>(student_exam.Submission);
                dataGrid.ItemsSource= ExamSubmissions;
            }

        }

        private void button_export_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_StudentsExams.SelectedItem != null)
            {
                var _studentExam = listBox_StudentsExams.SelectedItem as StudentExam;
                string textToSave = $"Exam Subbmition ID: {_studentExam.Id}\n" +
            $"Student ID: {_studentExam.StudentId}\nExam ID: {_studentExam.ExamId}\nGrade: {_studentExam.Grade}\n\n";

                foreach (var q in ExamSubmissions)
                {
                    textToSave += $"\nQustion: {q.Q}\nanswer: {q.SA}\nCorrent Answer: {q.CA}\n";
                }


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
}
