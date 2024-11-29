using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Data;
using AcademicNet.Interfaces;
using AcademicNet.DTO;
using AcademicNet.Models;

namespace AcademicNet.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AcademicNetDbContext _context;

        public SubjectRepository(AcademicNetDbContext context)
        {
            _context = context;
        }

        public async Task<SubjectDTO> AddSubject(SubjectDTO new_subject)
        {
            var subject = new SubjectModel{
                Name = new_subject.Name,
                Description = new_subject.Description,
                Grade = new_subject.Serie
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return new_subject;
        }
        
        public async Task<SubjectDTO> GetSubjectById(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                throw new KeyNotFoundException("Disciplina nao encontrada.");
            }
            return new SubjectDTO
            {
                Name = subject.Name,
                Description = subject.Description,
                Serie = subject.Grade
                
            };
        }

        public async Task<SubjectDTO> DeleteSubjectById(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                throw new KeyNotFoundException("Disciplina nao encontrada.");
            }
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return new SubjectDTO
            {
                Name = subject.Name,
                Description = subject.Description,
                Serie = subject.Grade
            };
        }
    
    }
}