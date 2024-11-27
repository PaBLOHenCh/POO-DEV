using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public class ClassSubjectModel
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int UnitId { get; set; }

        public float AVGGrade { get; private set; }
        public float AVGFrequency { get; private set; }

        public float AVGGradeFrequency { get; private set; }

        public ICollection<StudentSubjectModel> StudentSubjects { get; set; } = new List<StudentSubjectModel>();

        public ClassModel Class { get; set; }
        public SubjectModel Subject { get; set; }
        public TeacherModel Teacher { get; set; }
        public UnitModel Unit { get; set; }

        public float CalculateAverageGrade()
        {
            if (this.StudentSubjects != null && this.StudentSubjects.Any())
            {
                AVGGrade = (float)this.StudentSubjects.Average(x => x.Grade);
            }
            else
            {
                AVGGrade = 0;
            }
            return AVGGrade;
        }

        public float CalculateAverageFrequency()
        {
            if (this.StudentSubjects != null && this.StudentSubjects.Any())
            {
                AVGFrequency = (float)this.StudentSubjects.Average(x => x.Frequency);
                
            }
            else
            {
                AVGFrequency = 0;
            }
            return AVGFrequency;
        }

        public float CalculateAverageGradeFrequency()
        {
            this.AVGGradeFrequency = this.AVGGrade * this.AVGFrequency;
            return this.AVGGradeFrequency;
        }

    }
}