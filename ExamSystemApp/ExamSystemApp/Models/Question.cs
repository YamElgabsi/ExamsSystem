using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemApp.Models
{
    public class Question
    {
        public int Id { get; set; } // Question Number in the exam

        public bool IsPicture { get; set; }
        public string Q { get; set; } //Question
        public string A { get; set; } //Correct Answer
        public List<string> AllA { get; set; } //All the answers including the correct

        public Question() {
            this.Id = 1;
            this.IsPicture = false;
            this.Q = "Question";
            this.A = "Answer";
            this.AllA = new List<string>();
            this.AllA.Add(this.A);
        }

        public Question(int number, bool isPicture, string question, string answer, List<string> all_answer) {
            this.Id = number;
            this.IsPicture = isPicture;
            this.Q = question;
            this.A = answer;
            this.AllA = all_answer;
        }

        public int numberOfAnswers() { return this.AllA.Count; }


        public override string ToString()
        {
            return "Question " + Id;
        }
    }
}
