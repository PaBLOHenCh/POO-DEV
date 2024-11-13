using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Models;

namespace AcademicNet.DTO
{
    public class StudentCreateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string CPF { get; set; }
        public AddressDTO Address { get; set; }
        public IdentityRole Role { get; set; }
    }
}