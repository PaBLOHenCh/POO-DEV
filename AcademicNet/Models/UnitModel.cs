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

        public float AVGGradePerClassSubject { get; private set; }
        public float AVGGradePerClass { get; private set; }

        public float AVGGradeFrequencyPerClass { get; private set; }
        public float AVGGradeFrequencyPerClassSubject { get; private set; }

        public float AVGFrequencyPerClassSubject { get; private set; }
        public float AVGFrequencyPerClass { get; private set; }

        public float CalculateAverageGradePerClassSubject()
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

        public float CalculateAverageGradePerClass()
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

        public float CalculateAverageFrequencyPerClassSubject()
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

        public float CalculateAverageFrequencyPerClass()
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
        public float CalculateAverageGradeFrequencyPerClassSubject()
        {
            this.AVGGradeFrequencyPerClassSubject =this.AVGFrequencyPerClassSubject * this.AVGGradePerClassSubject;
            return this.AVGGradeFrequencyPerClassSubject;
        }

        public float CalculateAverageGradeFrequencyPerClass()
        {
            this.AVGGradeFrequencyPerClass = this.AVGFrequencyPerClass * this.AVGGradePerClass;
            return this.AVGGradeFrequencyPerClass;
        }
    }
}