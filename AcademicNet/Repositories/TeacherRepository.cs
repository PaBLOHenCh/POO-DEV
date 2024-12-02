using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Data;
using AcademicNet.DTO;
using AcademicNet.Interfaces;
using AcademicNet.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicNet.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AcademicNetDbContext _context;
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;

        public TeacherRepository(AcademicNetDbContext context, IClassRepository classRepository, IStudentRepository studentRepository)
        {
            _context = context;
            _classRepository = classRepository;
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<ClassSubjectModel>> GetClassSubject_byTeacherId(int id)
        {
            var classSubjects = await _context.ClassSubjects.Where(cs => cs.TeacherId == id).ToListAsync();

            if(classSubjects == null)
            {
                throw new KeyNotFoundException("Nenhuma disciplina encontrada para esse professor.");
            }
            return classSubjects;
        }

        public async Task<IEnumerable<StudentSubjectModel>> GetMatriculations_byClassSubjectId(int classId, int subjectId)
        {
            var matriculations = await _context.Matriculations
            .Where(m => m.ClassSubjectClassId == classId && m.SubjectId == subjectId)
            .ToListAsync();
            
            if(matriculations == null)
            {
                throw new KeyNotFoundException("Nenhuma matricula encontrada para esta disciplina.");
            }
            return matriculations;
        }

        public async Task<StudentSubjectModel> ThrowGrade(int studentId, int subjectId, float grade, float frequency)
        {
            var matriculation = await _context.Matriculations.FindAsync(studentId, subjectId);
            if(matriculation == null)
            {
                throw new KeyNotFoundException("Matricula nao encontrada.");
            }
            matriculation.Grade = grade;

            if(frequency < 0)
            {
                throw new ArgumentException("Frequencia nao pode ser negativa.");
            }
            else if(frequency > 1)
            {
                throw new ArgumentException("Frequencia nao pode ser maior que 1.");
            }
            matriculation.Frequency = frequency;

            matriculation.CalculateAverageGradeFrequency();
            await _context.SaveChangesAsync();

            await _classRepository.UpdateAVGGradeFrequency_byClassSubjectId(matriculation.ClassSubjectClassId, matriculation.SubjectId);
            await _studentRepository.UpdateAVGGradeFrequencyByStudentId(matriculation.StudentId);
            
            return matriculation;
        }

        public async Task<SubjectDTO> GaveSubject (int teacherId, int subjectId, int classId)
        {
            var classSubject = await _context.ClassSubjects
            .Include(cs => cs.Subject)
            .FirstAsync(cs => cs.ClassId == classId && cs.SubjectId == subjectId);
            if(classSubject == null)
            {
                throw new KeyNotFoundException("A grade da classe informada não contem essa disciplina.");
            }

            classSubject.TeacherId = teacherId;
            await _context.SaveChangesAsync();
            return new SubjectDTO{
                Name = classSubject.Subject.Name,
                Description = classSubject.Subject.Description,
                Serie = classSubject.Subject.Grade
            };

        }

    }
}