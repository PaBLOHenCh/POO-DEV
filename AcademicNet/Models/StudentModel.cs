using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AcademicNet.Models
{
    [Table("Students")]
    public class StudentModel : UserModel
    {
        public string? PathToPhotoProfile { get; set; }
        public float? AVGGrade { get; private set; }
        public float? AVGFrequency { get; private set; }

        public float? AVGGradeFrequency { get; private set; }
        
        [ForeignKey("Class")]
        public int? ClassId { get; set; }
        public ClassModel? Class { get; set; }
        
        public ICollection<StudentSubjectModel> StudentSubjects { get; set; } = new List<StudentSubjectModel>();
        public ICollection<StudentStudiesGroupModel> StudentStudiesGroups { get; set; } = new List<StudentStudiesGroupModel>();

        public float CalculateAverageGrade()
        {
            if (this.StudentSubjects != null && this.StudentSubjects.Any())
            {
                AVGGrade = (float)this.StudentSubjects.Average(ss => ss.Grade);
            }
            else
            {
                AVGGrade = 0; // Se não houver disciplinas, a média é 0
            }
            return AVGGrade.Value;
        }
        public float CalculateAverageFrequency()
        {
            if (this.StudentSubjects != null && this.StudentSubjects.Any())
            {
                AVGFrequency = (float)this.StudentSubjects.Average(ss => ss.Frequency);
            }
            else
            {
                AVGFrequency = 0; // Se não houver disciplinas, a média é 0
            }
            return AVGFrequency.Value;
        }

        public float CalculateAverageGradeFrequency()
        {
            if (this.StudentSubjects != null && this.StudentSubjects.Any())
            {
                AVGGradeFrequency = (float)this.StudentSubjects.Average(ss => ss.Grade * ss.Frequency);
            }
            else
            {
                AVGGradeFrequency = 0; // Se não houver disciplinas, a média é 0
            }
            return AVGGradeFrequency.Value;
        }
    }
}