using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;

namespace AcademicNet.Interfaces
{
    public interface IClassService
    {
        public Task<ClassDTO> CreateClass(ClassDTO new_class, int? coordinatorId, int? teacherId);
        public Task<ClassDTO> GetClassById(int? id);
        public Task<ClassDTO> DeleteClassById(int? id);
    }
}