using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    [Table("Teachers")]
    public class TeacherModel : UserModel
    {
        public string? PathToPhotoProfile { get; set; }
        public string? Degree { get; set; }
        public ICollection<ClassSubjectModel> ClassSubjects { get; set; } = new List<ClassSubjectModel>();
    }
}