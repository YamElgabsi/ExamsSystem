using ExamSystemApp.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExamSystemApp
{
    /// <summary>
    /// Interaction logic for TeacherNewExamDetailsWindow.xaml
    /// </summary>
    public partial class TeacherNewExamDetailsWindow : Window
    {
        User teacher;
        bool isNew;
        Exam _exam;
        public TeacherNewExamDetailsWindow(User user)
        {
            teacher= user;
            InitializeComponent();
            newExamDateTP.SelectedDate = DateTime.Now;
            newExamTimeWhenTB.Text = DateTime.Now.ToString("HH:mm");
            isNew = true;
        }

        public TeacherNewExamDetailsWindow(User user, Exam exam)
        {
            InitializeComponent();
            teacher = user;
            newExamNameTB.Text = exam.Name;
            newExamDateTP.SelectedDate = exam.Date;
            newExamTimeWhenTB.Text = exam.Date.ToString("HH:mm");
            newExamTimeTB.Text = exam.Minutes.ToString();
            newExamRandomRB.IsChecked = exam.IsRandom;
            isNew= false;
            _exam = exam;
        }

        public bool IsValidTime(string timeStr)
        {
            DateTime time;
            return DateTime.TryParseExact(timeStr, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
        }


        public void ChangeTime( ref DateTime dateTime, string timeString)
        {
            TimeSpan timeSpan = TimeSpan.ParseExact(timeString, "g", CultureInfo.InvariantCulture);
            // parse the time string as a TimeSpan object using the "g" format specifier

            // set the time component of the existing dateTime object to the new time
            dateTime = dateTime.Date + timeSpan;
        }



        private void newExamDetailsSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidTime(newExamTimeWhenTB.Text))
            {
                MessageBox.Show("Time exam need to be in HH:MM and valid");
                return;
            }
            DateTime time = newExamDateTP.SelectedDate.Value;

            ChangeTime(ref time, newExamTimeWhenTB.Text);




            bool? result;
            if(isNew)
            {
                string questions = string.Empty;
                if(_exam != null)
                {
                    questions= _exam.Questions;
                }
                Exam exam = new Exam(newExamNameTB.Text, teacher.Id, teacher.Name, time, int.Parse(newExamTimeTB.Text), newExamRandomRB.IsChecked.Value, questions);
                TeacherNewWindow teacherNewWindow = new TeacherNewWindow(teacher, exam, true);
                result = teacherNewWindow.ShowDialog();
            }
            else
            {
                _exam.Name = newExamNameTB.Text;
                _exam.Date = time;
                _exam.Minutes = int.Parse(newExamTimeTB.Text);
                _exam.IsRandom = newExamRandomRB.IsChecked.Value;
                TeacherNewWindow teacherNewWindow = new TeacherNewWindow(teacher, _exam, false);
                result = teacherNewWindow.ShowDialog();
            }

            // If the new window was closed, close this window
            if (result == false)
            {
                Close();
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                    Exam exam = JsonConvert.DeserializeObject<Exam>(fileContents);
                    if(_exam== null)
                    {
                        _exam = new Exam(exam);
                    }
                    else
                    {
                        _exam.Name = exam.Name;
                        _exam.Questions = exam.Questions;
                        _exam.Minutes = exam.Minutes;
                        _exam.IsRandom = exam.IsRandom;
                        _exam.Date = exam.Date;
                    }

                    //in case teacher take another thecher exam
                    exam.TeacherID = teacher.Id;     
                    exam.TeacherName = teacher.Name;

                    //update api
                    newExamNameTB.Text = exam.Name;
                    newExamDateTP.SelectedDate = exam.Date;
                    newExamTimeWhenTB.Text = exam.Date.ToString("HH:mm");
                    newExamTimeTB.Text = exam.Minutes.ToString();
                    newExamRandomRB.IsChecked = exam.IsRandom;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Not good file");
                }



            }
        }
    }
}
