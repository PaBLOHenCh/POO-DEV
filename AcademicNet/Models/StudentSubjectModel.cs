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
        public float Grade { get; set; }
        public float Frequency { get; set; }

        public float GradeFrequency { get; private set; }

        //chave estrangeira composta de class_subject
        public int ClassSubjectClassId { get; set; }
        public int ClassSubjectSubjectId { get; set; }

        //propriedades de navegação
        public ClassSubjectModel ClassSubject { get; set; }
        public StudentModel Student { get; set; }
        public SubjectModel Subject { get; set; }

        public float CalculateAverageGradeFrequency()
        {
            this.GradeFrequency = this.Grade * this.Frequency;
            return this.GradeFrequency;
        }

    }
}