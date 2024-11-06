using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    [Table("Matriculation")]
    public class StudentSubjectModel
    {
        
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public decimal Grade { get; set; }
        public float Frequency { get; set; }

        //chave estrangeira composta de class_subject
        public int ClassSubjectClassId { get; set; }
        public int ClassSubjectSubjectId { get; set; }

        //propriedade de navegação
        public ClassSubjectModel ClassSubject { get; set; }
    }
}