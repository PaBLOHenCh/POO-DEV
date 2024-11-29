using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public class ClassModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Grade { get; set; }
        public float? AVGGrade { get; private set; }
        public float? AVGFrequency { get; private set; }
        public float? AVGGradeFrequency { get; private set; }
        public string Name { get; set; }
        [ForeignKey("Coordinator")]
        public int CoordinatorId {get; set;}
        public CoordinatorModel Coordinator {get; set;}

        [ForeignKey("Unit")]
        public int UnitId {get; set;}
        public UnitModel Unit {get; set;}
        public ICollection<ClassSubjectModel> ClassSubjects {get; set;} = new List<ClassSubjectModel>();
        public ICollection<StudentModel> Students {get; set;} = new List<StudentModel>();

        public float CalculateAverageGrade()
        {
            if (this.ClassSubjects != null && this.ClassSubjects.Any())
            {
                AVGGrade = (float) this.ClassSubjects.Average(x => x.AVGGrade);
            }
            else
            {
                AVGGrade = 0;
            }
            return AVGGrade.Value;
        }

        public float CalculateAverageFrequency()
        {
            if (this.ClassSubjects != null && this.ClassSubjects.Any())
            {
                AVGFrequency = (float) this.ClassSubjects.Average(x => x.AVGFrequency);
            }
            else
            {
                AVGFrequency = 0;
            }

            return AVGFrequency.Value;
        }

        public float CalculateAverageGradeFrequency()
        {
            this.AVGGradeFrequency = this.AVGGrade * this.AVGFrequency;
            return this.AVGGradeFrequency.Value;
        }

    }
}