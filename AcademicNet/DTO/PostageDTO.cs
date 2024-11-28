using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Models;

namespace AcademicNet.DTO
{
    public class PostageDTO
    {
        public string StudentName { get; set; }
        public string? TextBody { get; set; }
        public string? PathToPhoto { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<PostageModel>? Replies { get; set; }

    }
}