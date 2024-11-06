using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public class ClassSubjectModel
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int UnitId { get; set; }
        public ICollection<StudentSubjectModel> StudentSubjects { get; set; } = new List<StudentSubjectModel>();

        public ClassModel Class { get; set; }
        public SubjectModel Subject { get; set; }
        public TeacherModel Teacher { get; set; }
        public UnitModel Unit { get; set; }

    }
}