using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.DTO
{
    public class RankingDTO
    {
        public string? StudentName { get; set; }
        public string? ClassName { get; set; }
        public string? UnitName { get; set; }
        public decimal GradeFrequency { get; set; }
    }
    
}