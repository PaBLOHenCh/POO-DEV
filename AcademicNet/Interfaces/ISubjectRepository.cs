using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;

namespace AcademicNet.Interfaces
{
    public interface ISubjectRepository
    {
        public Task<SubjectDTO> AddSubject(SubjectDTO new_subject);
        public Task<SubjectDTO> GetSubjectById(int id);
        public Task<SubjectDTO> DeleteSubjectById(int id);
    }
}