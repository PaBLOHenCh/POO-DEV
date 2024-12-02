using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicNet.Models
{
    public class UnitModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        /* public float AVGGradePerClass { get; private set; }
        public float AVGGradePerClassSubject { get; private set; } */
        public ICollection<ClassSubjectModel> ClassSubjects { get; set; } = new List<ClassSubjectModel>();
        public ICollection<ClassModel> Classes { get; set; } = new List<ClassModel>();
        public ICollection<CoordinatorModel> Coordinators { get; set; } = new List<CoordinatorModel>();

        public float AVGGradePerClassSubject { get; private set; }
        public float AVGGradePerClass { get; private set; }

        public float AVGGradeFrequencyPerClass { get; private set; }
        public float AVGGradeFrequencyPerClassSubject { get; private set; }

        public float AVGFrequencyPerClassSubject { get; private set; }
        public float AVGFrequencyPerClass { get; private set; }

        public void UpdateAllGradesAndFrequencies()
        {
            this.CalculateAverageGradePerClassSubject();
            this.CalculateAverageGradePerClass();
            this.CalculateAverageFrequencyPerClassSubject();
            this.CalculateAverageFrequencyPerClass();
            this.CalculateAverageGradeFrequencyPerClassSubject();
            this.CalculateAverageGradeFrequencyPerClass();
        }

        private float CalculateAverageGradePerClassSubject()
        {
            if (this.ClassSubjects != null && this.ClassSubjects.Any())
            {
                AVGGradePerClassSubject = (float)this.ClassSubjects.Average(x => x.AVGGrade);
            }
            else
            {
                AVGGradePerClassSubject = 0;
            }

            return AVGGradePerClassSubject;
        }

        private float CalculateAverageGradePerClass()
        {
            if (this.Classes != null && this.Classes.Any())
            {
                AVGGradePerClass = (float)this.Classes.Average(x => x.AVGGrade);
            }
            else
            {
                AVGGradePerClass = 0;
            }
            return AVGGradePerClass;
        }

        private float CalculateAverageFrequencyPerClassSubject()
        {
            if (this.ClassSubjects != null && this.ClassSubjects.Any())
            {
                AVGFrequencyPerClassSubject = (float)this.ClassSubjects.Average(x => x.AVGFrequency);
            }
            else
            {
                AVGFrequencyPerClassSubject = 0;
            }
            return AVGFrequencyPerClassSubject;
        }

        private float CalculateAverageFrequencyPerClass()
        {
            if (this.Classes != null && this.Classes.Any())
            {
                AVGFrequencyPerClass = (float)this.Classes.Average(x => x.AVGFrequency);
            }
            else
            {
                AVGFrequencyPerClass = 0;
            }
            return AVGFrequencyPerClass;
        }
        private float CalculateAverageGradeFrequencyPerClassSubject()
        {
            this.AVGGradeFrequencyPerClassSubject =this.AVGFrequencyPerClassSubject * this.AVGGradePerClassSubject;
            return this.AVGGradeFrequencyPerClassSubject;
        }

        private float CalculateAverageGradeFrequencyPerClass()
        {
            this.AVGGradeFrequencyPerClass = this.AVGFrequencyPerClass * this.AVGGradePerClass;
            return this.AVGGradeFrequencyPerClass;
        }
    }
}