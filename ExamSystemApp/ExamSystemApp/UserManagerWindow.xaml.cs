using ExamSystemApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
    /// Interaction logic for UserManagerWindow.xaml
    /// </summary>
    public partial class UserManagerWindow : Window
    {
        HttpClient clientApi;

        User? user;
        List<Exam>? exams;
        List<StudentExam>? studentExams;

        public UserManagerWindow()
        {
            InitializeComponent();

            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7002");
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

        public async Task GetUser(string id)
        {
            user = null;
            HttpResponseMessage response = clientApi.GetAsync("api/Users/" + id).Result;
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var m = JsonConvert.DeserializeObject<User>(jsonResponse);
                user = m;
            }
        }

        public async Task CreateUser()
        {

            string jsonString = JsonConvert.SerializeObject(user);


            using (var content = new StringContent(jsonString, Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result = clientApi.PostAsync($"api/Users", content).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    label_Result.Foreground = Brushes.Green;
                    label_Result.Content = $"Created user to {user.Name}";
                    return;

                }

                else
                {
                    string returnValue = result.Content.ReadAsStringAsync().Result;
                    label_Result.Foreground = Brushes.Red;
                    label_Result.Content = $"Failed to POST data: ({result.StatusCode}): {returnValue}";
                    return;
                }

            }

        }

        public async Task DeleteStudent() 
        {
            //Get all StudentExams
            HttpResponseMessage response = clientApi.GetAsync("api/StudentExams").Result;
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var m = JsonConvert.DeserializeObject<List<StudentExam>>(jsonResponse);
                studentExams = m;
            }
            else
            {
                MessageBox.Show("Error");
                return;
            }

            //Delete all user's exams
            foreach( StudentExam studentExam in studentExams )
            {
                if(studentExam.StudentId==user.Id)
                {
                     clientApi.DeleteAsync("api/StudentExams/" + studentExam.Id);
                }
            }


            //Delete the user from the detabase
            var response2 = clientApi.DeleteAsync("api/Users/" + user.Id).Result;
            if (response2.IsSuccessStatusCode)
            {
                label_Result.Foreground = Brushes.Green;
                label_Result.Content = $"{user.Name} Deleted!";
                return;
            }
            else
                MessageBox.Show("Error");
        }

        public async Task DeleteTeacher()
        {
            //Get all StudentExams
            HttpResponseMessage response = clientApi.GetAsync("api/StudentExams").Result;
            if (response != null)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var m = JsonConvert.DeserializeObject<List<StudentExam>>(jsonResponse);
                studentExams = m;
            }
            else
            {
                MessageBox.Show("Error");
                return;
            }

            //delete all sturdens exams
            foreach (StudentExam studentExam in studentExams)
            {
                if (studentExam.TeacherId == user.Id)
                {
                    clientApi.DeleteAsync("api/StudentExams/" + studentExam.Id);
                }
            }


            //Get all Exams
            HttpResponseMessage response2 = clientApi.GetAsync("api/Exams").Result;
            if (response2 != null)
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var m = JsonConvert.DeserializeObject<List<Exam>>(jsonResponse);
                exams = m;
            }
            else
            {
                MessageBox.Show("Error");
                return;
            }

            //Delete all user's exams
            foreach (Exam exam in exams)
            {
                clientApi.DeleteAsync("api/Exams/" + exam.ExamId);
            }


            //Delete the user from the detabase
            var response3 = clientApi.DeleteAsync("api/Users/" + user.Id).Result;
            if (response3.IsSuccessStatusCode)
            {
                label_Result.Foreground = Brushes.Green;
                label_Result.Content = $"{user.Name} Deleted!";
                return;
            }
            else
                MessageBox.Show("Error");
        }

        private void button_Create_Click(object sender, RoutedEventArgs e)
        {
            string id = textBox_ID_New.Text;

            _ = GetUser(id);
            if(user!= null)
            {
                label_Result.Foreground = Brushes.Red;
                label_Result.Content = $"User with ID:{id} allready exist!";
                return;
            }
            
            if(!(id.Length == 9 && id.All(char.IsDigit)))
            {
                label_Result.Foreground = Brushes.Red;
                label_Result.Content = $"New User ID:{id} Not Valid!\nNeed to be 9 length and all digit (0-9)";
                return;
            }

            string password = textBox_Password.Text;
            if(password == "")
            {
                label_Result.Foreground = Brushes.Red;
                label_Result.Content = "Password can not be empty!";
                return;
            }

            string name = textBox_Name.Text;
            if (password == "")
            {
                label_Result.Foreground = Brushes.Red;
                label_Result.Content = "Name can not be empty!";
                return;
            }

            user = new User(id, password, name, radioButton_isTeacher.IsChecked.Value);

            _ = CreateUser();
            user = null;
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            string id = textBox_ID_Delete.Text;

            _ = GetUser(id);
            if (user == null)
            {
                label_Result.Foreground = Brushes.Red;
                label_Result.Content = $"User with ID:{id} Not Found!";
                return;
            }

            if (user.IsTeacher) DeleteTeacher();
            else DeleteStudent();

            user = null;

        }
    }
}
