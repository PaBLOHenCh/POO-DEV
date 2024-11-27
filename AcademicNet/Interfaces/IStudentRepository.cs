using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Models;

namespace AcademicNet.Interfaces
{
    public interface IStudentRepository
    {
        public Task<StudentModel> AddStudent(StudentModel student);
        public Task<StudentModel> GetStudentById(int id);
        public Task<StudentModel> DeleteStudentById(int id);

        public IEnumerable<RankingDTO> GetRanking_AVGStudentUniversal();
        public IEnumerable<RankingDTO> GetRanking_Student_perSubject(int subjectId);
        public IEnumerable<RankingDTO> GetRanking_Student_perUnit_Class(int unitId, int classId);
        public IEnumerable<RankingDTO> GetRanking_Student_perUnit_Class_Subject(int unitId, int classId, int subjectId);
        public IEnumerable<RankingDTO> GetRanking_Student_perUnit_Subject(int unitId, int subjectId);
        public IEnumerable<RankingDTO> GetRanking_Student_perUnit(int unitId);


        public IEnumerable<RankingDTO> GetRanking_Class_perSubject_Universal(int subjectId);
        public IEnumerable<RankingDTO> GetRanking_Class_Universal();
        public IEnumerable<RankingDTO> GetRanking_Unit_Class_perSubject(int unitId, int subjectId);
        public IEnumerable<RankingDTO> GetRanking_Unit_Class_No_Subject(int unitId);
        public IEnumerable<RankingDTO> GetRanking_Unit_Class(int unitId, int classId);


        public IEnumerable<RankingDTO> GetRanking_Unit_Universal();
        public IEnumerable<RankingDTO> GetRanking_Unit_perSubject(int subjectId);


        
    }
}