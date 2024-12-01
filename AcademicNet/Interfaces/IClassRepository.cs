using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Models;

namespace AcademicNet.Interfaces
{
    public interface IClassRepository
    {
        public Task<ClassDTO> CreateClass(ClassDTO new_class, int coordinatorId, int teacherId);
        public Task<ClassDTO> GetClassById(int id);
        public Task<ClassDTO> DeleteClassById(int id);
        public Task<ClassModel> UpdateAVGGradeFrequency_byClassId(int classId);
        public Task<ClassSubjectModel> UpdateAVGGradeFrequency_byClassSubjectId(int classId, int subjectId);
    }
}