using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public class StudiesGroupModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<StudentStudiesGroupModel> StudentStudiesGroups { get; set; } = new List<StudentStudiesGroupModel>();
    }
}