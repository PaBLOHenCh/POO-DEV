using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Interfaces;
using AcademicNet.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AcademicNet.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<SubjectDTO> AddSubject(SubjectDTO new_subject)
        {
            if(new_subject == null)
            {
                throw new ArgumentNullException("Disciplina nao pode ser nula.");
            }
            return await _subjectRepository.AddSubject(new_subject);
        }

        public async Task<SubjectDTO> GetSubjectById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Id nao pode ser nulo.");
            }

            return await _subjectRepository.GetSubjectById(id.Value);
        }
    
        public async Task<SubjectDTO> DeleteSubjectById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Id nao pode ser nulo.");
            }
            try
            {
                return await _subjectRepository.DeleteSubjectById(id.Value);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            
        }
    
    }
}