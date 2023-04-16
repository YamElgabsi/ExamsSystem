using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.ClassLibary
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ExamId { get; set; }
        public string Name { get; set; }
        public string TeacherID { get; set; }
        public string TeacherName { get; set; }
        public DateTime Date { get; set; }
        public int Minutes { get; set; }
        public bool IsRandom { get; set; }
        public string Questions { get; set; }

    }

}
