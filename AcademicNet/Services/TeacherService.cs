using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Interfaces;
using AcademicNet.Models;

namespace AcademicNet.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;


        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<ClassSubjectModel>> GetClassSubject_byTeacherId(int? id)
        {
            if(id == null)
            {
                throw new ArgumentNullException("Id nao pode ser nulo.");
            }
            return await _teacherRepository.GetClassSubject_byTeacherId(id.Value);
        }

        public async Task<IEnumerable<StudentSubjectModel>> GetMatriculations_byClassSubjectId(int? classId, int? subjectId)
        {
            if(classId == null || subjectId == null)
            {
                throw new ArgumentNullException("Classe ou disciplina não encotradas.");
            }
            return await _teacherRepository.GetMatriculations_byClassSubjectId(classId.Value, subjectId.Value);
        }

        public async Task<StudentSubjectModel> ThrowGrade(int? studentId, int? subjectId, float? grade, float? frequency)
        {
            if (studentId == null || subjectId == null || grade == null)
            {
                throw new ArgumentNullException("Id nulo ou nota nula.");
            }

            return await _teacherRepository.ThrowGrade(studentId.Value, subjectId.Value, grade.Value, frequency.Value);
        }
    }
}