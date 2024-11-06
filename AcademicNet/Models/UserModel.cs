using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public enum IdentityRole { Student, Teacher, Coordinator }

    public abstract class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public AddressModel Address { get; set; }
        public IdentityRole Role { get; set; }
    }
}