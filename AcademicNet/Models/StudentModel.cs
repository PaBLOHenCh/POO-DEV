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
        public string PathToPhotoProfile { get; set; }
        public float AVGGrade { get; private set; }
        public float AVGFrequency { get; private set; }
        
        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public ClassModel Class { get; set; }
        public ICollection<StudentSubjectModel> StudentSubjects { get; set; } = new List<StudentSubjectModel>();
        public ICollection<StudentStudiesGroupModel> StudentStudiesGroups { get; set; } = new List<StudentStudiesGroupModel>();

        public void CalculateAverageGrade(IEnumerable<StudentSubjectModel> studentSubjects)
        {
            if (studentSubjects != null && studentSubjects.Any())
            {
                AVGGrade = (float)studentSubjects.Average(ss => ss.Grade);
            }
            else
            {
                AVGGrade = 0; // Se não houver disciplinas, a média é 0
            }
        }
        public void CalculateAverageFrequency(IEnumerable<StudentSubjectModel> studentSubjects)
        {
            if (studentSubjects != null && studentSubjects.Any())
            {
                AVGFrequency = (float)studentSubjects.Average(ss => ss.Frequency);
            }
            else
            {
                AVGFrequency = 0; // Se não houver disciplinas, a média é 0
            }
        }
    }
}