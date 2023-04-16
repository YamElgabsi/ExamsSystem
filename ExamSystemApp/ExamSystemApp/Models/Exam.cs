using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ExamSystemApp.Models
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ExamId { get; set; }
        public string Name { get; set; }
        public string TeacherID { get; set; }
        public string TeacherName { get;set; }
        public DateTime Date { get; set; }
        public int Minutes { get; set; }
        public bool IsRandom { get; set; }
        public string Questions { get; set; }

        public Exam(string name, string teacher_id, string teacher_name, DateTime dateTime, int longE, bool isRan, string questions) { 
            ExamId= Guid.NewGuid().ToString();
            Name= name;
            TeacherID= teacher_id;
            TeacherName= teacher_name;
            Date = dateTime;
            Minutes= longE;
            Questions= questions;
        }

        public Exam(Exam rhs)
        {
            ExamId = Guid.NewGuid().ToString();
            Name = rhs.Name;
            TeacherID = rhs.TeacherID;
            TeacherName = rhs.TeacherName;
            Date = rhs.Date;
            Minutes = rhs.Minutes;
            Questions = rhs.Questions;
        }
        public Exam()
        {
            ExamId = Guid.NewGuid().ToString();
            Name = string.Empty;
            TeacherID = string.Empty;
            TeacherName = string.Empty;
            Date = DateTime.Now;
            Minutes = 120;
            Questions = string.Empty;
        }

        public override string ToString()
        {
            return  Name + " By " + TeacherName;
        }
    }
    

}
