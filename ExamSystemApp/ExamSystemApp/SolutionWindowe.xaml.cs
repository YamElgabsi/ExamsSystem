using ExamSystemApp.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// show student test results 
    /// </summary>
    public partial class SolutionWindowe : Window
    {
        public List<ExamSubmission> ExamSubmissions { get; set; }

        StudentExam _studentExam;
        public SolutionWindowe(StudentExam studentExam)
        {
            InitializeComponent();
            _studentExam = studentExam;
            label_grade.Content = studentExam.Grade.ToString();
            ExamSubmissions = JsonSerializer.Deserialize<List<ExamSubmission>>(_studentExam.Submission);
            DataContext = this;

        }

        private void EXPORT_Click(object sender, RoutedEventArgs e)
        {
            // the student has an option to save his solution to txt file
            string textToSave = $"Exam Subbmition ID: {_studentExam.Id}\n" +
            $"Student ID: {_studentExam.StudentId}\nExam ID: {_studentExam.ExamId}\nGrade: {_studentExam.Grade}\n\n";

            foreach (var q in ExamSubmissions)
            {
                textToSave += $"\nQustion: {q.Q}\nanswer: {q.SA}\nCorrent Answer: {q.CA}\n";
            }


            

            // Get folder path and create/save text file
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
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
