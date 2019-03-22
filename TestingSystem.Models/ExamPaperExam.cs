using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Models
{
    public class ExamPaperExam
    {
        public int Id { set; get; }

        public int ExamID { set; get; }

        public int ExamPaperID { set; get; }

        public virtual Exam Exam { set; get; }

        public virtual ExamPaper ExamPaper { set; get; }
             
    }
}
