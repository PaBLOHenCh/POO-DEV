using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    [Table("Subjects")]
    public class SubjectModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //parametro que indica qual série a disciplina pertence
        public int Grade { get; set; }
        public ICollection<StudentSubjectModel> StudentSubjects { get; set; } = new List<StudentSubjectModel>();
        public ICollection<ClassSubjectModel> ClassSubjects { get; set; } = new List<ClassSubjectModel>();
        

    }
}