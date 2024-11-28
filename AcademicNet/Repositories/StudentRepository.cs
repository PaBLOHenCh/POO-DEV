using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Interfaces;
using AcademicNet.DTO;
using AcademicNet.Data;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AcademicNet.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AcademicNetDbContext _context;

        public StudentRepository(AcademicNetDbContext context)
        {
            _context = context;
        }

        public async Task<StudentModel> AddStudent(StudentModel student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<StudentModel> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return null;
            }

            return student;
        }

        public async Task<StudentModel> DeleteStudentById(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return student;
            
        }


        //rankings de estudante

        public async Task<IEnumerable<RankingDTO>> GetRanking_AVGStudentUniversal()
        {

            IQueryable<RankingDTO> query = _context.Students
                .Include(s => s.StudentSubjects)
                .Include(s => s.Class)
                .OrderByDescending(s => s.AVGGradeFrequency ?? 0)
                .Select(s => new RankingDTO
                {
                    StudentName = s.Name,
                    GradeFrequency = s.AVGGradeFrequency ?? 0,
                    ClassName = s.Class.Name
                })
                .Take(100);

                var result = await query.ToListAsync();

 
                return result;
        }
       
        public async Task<IEnumerable<RankingDTO>> GetRanking_Student_perSubject(int subjectId)
        {
            return await _context.Matriculations.Where(m => m.SubjectId == subjectId)
                .OrderByDescending(m => m.GradeFrequency)
                .Select(m => new RankingDTO
                {
                    StudentName = m.Student.Name,
                    GradeFrequency = m.GradeFrequency,
                    ClassName = m.ClassSubject.Class.Name,
                    UnitName = m.ClassSubject.Unit.Name
                })
                .Take(100)
                .ToListAsync();
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_Student_perUnit_Class(int unitId, int classId)
        {
            return await _context.Students.Where(s => s.ClassId == classId && s.Class.UnitId == unitId)
                .OrderByDescending(s => s.AVGGradeFrequency ?? 0)
                .Select(s => new RankingDTO
                {
                    StudentName = s.Name,
                    GradeFrequency = s.AVGGradeFrequency ?? 0,
                    ClassName = s.Class.Name
                })
                .Take(100)
                .ToListAsync();
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_Student_perUnit_Class_Subject(int unitId, int classId, int subjectId)
        {
            return await _context.Matriculations.Where(m => m.ClassSubject.UnitId == unitId && m.ClassSubject.ClassId == classId && m.SubjectId == subjectId)
                .OrderByDescending(m => m.GradeFrequency)
                .Select(m => new RankingDTO
                {
                    StudentName = m.Student.Name,
                    GradeFrequency = m.GradeFrequency,
                    ClassName = m.ClassSubject.Class.Name,
                    UnitName = m.ClassSubject.Unit.Name
                })
                .Take(100)
                .ToListAsync();
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_Student_perUnit_Subject(int unitId, int subjectId)
        {
            return await _context.Matriculations.Where(m => m.ClassSubject.UnitId == unitId && m.SubjectId == subjectId)
                .OrderByDescending(m => m.GradeFrequency)
                .Select(m => new RankingDTO
                {
                    StudentName = m.Student.Name,
                    GradeFrequency = m.GradeFrequency,
                    ClassName = m.ClassSubject.Class.Name,
                })
                .Take(100)
                .ToListAsync();
        }
        public async Task<IEnumerable<RankingDTO>> GetRanking_Student_perUnit(int unitId)
        {
            return await _context.Students.Where(s => s.Class.UnitId == unitId)
                .OrderByDescending(s => s.AVGGradeFrequency ?? 0)
                .Select(s => new RankingDTO
                {
                    StudentName = s.Name,
                    GradeFrequency = s.AVGGradeFrequency ?? 0,
                    ClassName = s.Class.Name,
                    UnitName = s.Class.Unit.Name
                })
                .Take(100)
                .ToListAsync();
        }
        


        //rankings de classe


        public async Task<IEnumerable<RankingDTO>> GetRanking_Class_perSubject_Universal(int subjectId)
        {
            return await _context.ClassSubjects.Where(cs => cs.SubjectId == subjectId)
                .OrderByDescending(cs => cs.AVGGradeFrequency)
                .Select(cs => new RankingDTO
                {
                    ClassName = cs.Class.Name,
                    UnitName = cs.Unit.Name,
                    GradeFrequency = cs.AVGGradeFrequency
                })
                .Take(100)
                .ToListAsync();
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_Class_Universal()
        {
            return await _context.Classes
                .OrderByDescending(c => c.AVGGradeFrequency)
                .Select(c => new RankingDTO
                {
                    ClassName = c.Name,
                    UnitName = c.Unit.Name,
                    GradeFrequency = c.AVGGradeFrequency
                })
                .Take(100)
                .ToListAsync();
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_Unit_Class_perSubject(int unitId, int subjectId)
        {
            return await _context.Classes.Where(c => c.UnitId == unitId && c.ClassSubjects.Any(cs => cs.SubjectId == subjectId))
                .OrderByDescending(c => c.AVGGradeFrequency)
                .Select(c => new RankingDTO
                {
                    ClassName = c.Name,
                    UnitName = c.Unit.Name,
                    GradeFrequency = c.AVGGradeFrequency
                })
                .Take(100)
                .ToListAsync();
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_Unit_Class_No_Subject(int unitId)
        {
            return await _context.Classes.Where(c => c.UnitId == unitId)
                .OrderByDescending(c => c.AVGGradeFrequency)
                .Select(c => new RankingDTO
                {
                    ClassName = c.Name,
                    UnitName = c.Unit.Name,
                    GradeFrequency = c.AVGGradeFrequency
                })
                .Take(100)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<RankingDTO>> GetRanking_Unit_Class(int unitId, int classId)
        {
            return await _context.Classes.Where(c => c.UnitId == unitId && c.Id == classId)
                .OrderByDescending(c => c.AVGGradeFrequency)
                .Select(c => new RankingDTO
                {
                    ClassName = c.Name,
                    UnitName = c.Unit.Name,
                    GradeFrequency = c.AVGGradeFrequency
                })
                .Take(100)
                .ToListAsync();
        }


        //rankings de unidade

        public async Task<IEnumerable<RankingDTO>> GetRanking_Unit_Universal()
        {
            return await _context.Units
                .OrderByDescending(u => u.AVGGradeFrequencyPerClass)
                .Select(u => new RankingDTO
                {
                    UnitName = u.Name,
                    GradeFrequency = u.AVGGradeFrequencyPerClass
                })
                .Take(100)
                .ToListAsync();
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_Unit_perSubject(int subjectId)
        {
            return await _context.Units.Where(u => u.ClassSubjects.Any(cs => cs.SubjectId == subjectId))
                .OrderByDescending(u => u.AVGGradeFrequencyPerClassSubject)
                .Select(u => new RankingDTO
                {
                    UnitName = u.Name,
                    GradeFrequency = u.AVGGradeFrequencyPerClassSubject
                })
                .Take(100)
                .ToListAsync();
        }


        public async Task<IEnumerable<StudentSubjectModel>> GetStudentSubjectByStudentId(int id)
        {
            return  await _context.Matriculations.Where(m => m.StudentId == id).ToListAsync();
        }
    }
}