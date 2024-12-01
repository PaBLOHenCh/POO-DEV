using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Models;

namespace AcademicNet.Interfaces
{
    public interface ITeacherService
    {
        public Task<IEnumerable<ClassSubjectModel>> GetClassSubject_byTeacherId(int? id);
        public Task<IEnumerable<StudentSubjectModel>> GetMatriculations_byClassSubjectId(int? classId, int? subjectId);
        public Task<StudentSubjectModel> ThrowGrade(int? studentId, int? subjectId, float? grade, float? frequency);
    }
}