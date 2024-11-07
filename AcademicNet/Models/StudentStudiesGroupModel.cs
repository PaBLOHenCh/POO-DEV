using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public class StudentStudiesGroupModel
    {
        public int StudentId { get; set; }
        public StudentModel Student { get; set; }
        public int StudiesGroupId { get; set; }
        public StudiesGroupModel StudiesGroup { get; set; }
        public ICollection<PostageModel> Postages { get; set; } = new List<PostageModel>();
        
    }
}