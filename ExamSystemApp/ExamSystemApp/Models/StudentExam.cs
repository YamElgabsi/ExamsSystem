using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemApp.Models
{
    public class StudentExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string ExamId { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }

        public string TeacherId { get; set; }
        public int Grade { get; set; }

        public string Submission { get; set; }

        public StudentExam(string exam_id, string student_id, string student_name, string teacher_id, int exam_grade, string sub)
        {

            Id = Guid.NewGuid().ToString();
            ExamId = exam_id;
            StudentId = student_id;
            StudentName = student_name;
            TeacherId = teacher_id;
            Grade = exam_grade;
            Submission = sub;

        }

        public StudentExam()
        {
            Id = string.Empty;
            ExamId= string.Empty;
            StudentId = string.Empty;
            StudentName = string.Empty;
            TeacherId = string.Empty;
            Grade = 0;
            Submission = string.Empty;
        }

        public override string ToString()
        {
            return StudentId + " | "+ StudentName;
        }
    }

    public class ExamSubmission
    {
        public string Q { get; set; }   //Question
        public string CA { get; set; }  //Correct Answer
        public string SA { get; set; }  ///Student Answer

    }
}
