using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public class ClassModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Coordinator")]
        public int CoordinatorId {get; set;}
        public CoordinatorModel Coordinator {get; set;}

        [ForeignKey("Unit")]
        public int UnitId {get; set;}
        public UnitModel Unit {get; set;}
        public float AVGGradePerStudent {get; private set;}
        public ICollection<ClassSubjectModel> ClassSubjects {get; set;} = new List<ClassSubjectModel>();
        public ICollection<StudentModel> Students {get; set;} = new List<StudentModel>();
    }
}