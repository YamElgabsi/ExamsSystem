using ExamSystemApp.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExamSystemApp.Models;
using Newtonsoft.Json;

namespace ExamSystemApp
{
    public partial class LoginWindow : Window
    {
        HttpClient clientApi;
        User? user;

        public LoginWindow()
        {
            InitializeComponent();
            clientApi = new HttpClient();
            clientApi.BaseAddress = new Uri("https://localhost:7002");
            user = null;
        }

        //Get user from database, if not found get null
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


        private void signinBTN_Click(object sender, RoutedEventArgs e)
        {
            string id = userIdTB.Text;
            string password = userPasswordTB.Text;

            //login to admin user that create and delete users
            if(id == "admin" && password == "admin")
            {
                UserManagerWindow userManagerWindow = new UserManagerWindow();
                userManagerWindow.ShowDialog();
                errorMessageTB.Text = "";
                return;
            }


            _ = GetUser(id);

            if (user == null || password != user.Password)  //if id not found or the pasword do not match
            {
                errorMessageTB.Text = ("Invalid Input!");
                return;
            }


            bool? result;
            errorMessageTB.Text = "";

            if (!user.IsTeacher)
            {
                // if user is student - open student window
                StudentWindow studentWindow = new StudentWindow(user);
                studentWindow.Owner= this;
                result =  studentWindow.ShowDialog();

              
            }
            else
            {
                // if user is teacher - open teacher window
                TeacherWindow teacherWindow = new TeacherWindow(user);
                teacherWindow.Owner= this;
                result = teacherWindow.ShowDialog();
            }

            
        }

    }

}
