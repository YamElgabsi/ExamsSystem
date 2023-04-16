using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemApp.Models
{
    public class User
    { 
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsTeacher { get; set; }

        public User(string id, string password, string name, bool isTeacher)
        {
            Id = id;
            Password = password;
            Name = name;
            IsTeacher = isTeacher;
        }



    }
}
        