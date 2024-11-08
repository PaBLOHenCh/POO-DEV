using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public class UnitModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        /* public float AVGGradePerClass { get; private set; }
        public float AVGGradePerClassSubject { get; private set; } */
        public ICollection<ClassSubjectModel> ClassSubjects { get; set; } = new List<ClassSubjectModel>();
        public ICollection<ClassModel> Classes { get; set; } = new List<ClassModel>();


    }
}