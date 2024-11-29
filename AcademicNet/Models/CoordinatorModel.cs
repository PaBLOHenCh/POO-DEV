using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    [Table("Coordinators")]
    public class CoordinatorModel : UserModel
    {
        public ICollection<ClassModel> Classes { get; set; } = new List<ClassModel>();
        public UnitModel? Unit { get; set; }
        public int? UnitId { get; set; }
    }
}