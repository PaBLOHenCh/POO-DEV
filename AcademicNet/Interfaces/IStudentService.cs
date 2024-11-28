using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Models;

namespace AcademicNet.Interfaces
{
    public interface IStudentService
    {
        public Task<StudentModel> AddStudent(StudentCreateDTO student);
        public Task<StudentModel> GetStudentById(int id);
        public Task<StudentModel> DeleteStudentById(int id);

        public Task<IEnumerable<RankingDTO>> GetRanking_perStudent(int? unitId, int? classId, int? subjectId);

        public Task<IEnumerable<RankingDTO>> GetRanking_per_Class(int? unitId, int? classId, int? subjectId);

        public Task<IEnumerable<RankingDTO>> GetRanking_perUnit(int? subjectId);

    }
}