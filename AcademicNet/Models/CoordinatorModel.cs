using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AcademicNet.Models
{
    [Table("Coordinators")]
    public class CoordinatorModel : UserModel
    {
        public ICollection<ClassModel> Classes { get; set; }
    }
}