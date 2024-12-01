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
    public class ClassRepository : IClassRepository
    {
        private readonly AcademicNetDbContext _context;
        private readonly IUnitRepository _unitRepository;

        public ClassRepository(AcademicNetDbContext context, IUnitRepository unitRepository)
        {
            _context = context;
            _unitRepository = unitRepository;
        }

        public async Task<ClassDTO> CreateClass(ClassDTO new_class, int coordinatorId, int teacherId)
        {
            //usando a transação para operações atomicas e completas
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //preparando classe
                var (newClass, unitId) = PrepareClass(new_class, coordinatorId);

                //salvando classe no banco
                await _context.Classes.AddAsync(newClass);
                await _context.SaveChangesAsync();

                //preparando classSubjects associadas à série da classe(grade)
                var ClassSubject = PrepareClassSubjects(newClass, unitId, teacherId);

                //salvando classSubjects no banco
                await _context.ClassSubjects.AddRangeAsync(ClassSubject);
                await _context.SaveChangesAsync();

                //commit de transação
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }

            return new_class;
        }

        public async Task<ClassDTO> GetClassById(int id)
        {
            var classModel = await _context.Classes.FindAsync(id);

            if (classModel == null)
            {
                throw new KeyNotFoundException("Classe nao encontrada.");
            }

            return new ClassDTO
            {
                Name = classModel.Name,
                Serie = classModel.Grade
            };
        }

        public async Task<ClassDTO> DeleteClassById(int id)
        {
            var classModel = await _context.Classes.FindAsync(id);
            if (classModel == null)
            {
                throw new KeyNotFoundException("Classe nao encontrada.");
            }
            _context.Classes.Remove(classModel);
            await _context.SaveChangesAsync();
            return new ClassDTO
            {
                Name = classModel.Name,
                Serie = classModel.Grade
            };
        }

        public async Task<ClassModel> UpdateAVGGradeFrequency_byClassId(int classId)
        {
            var classModel = await _context.Classes.FindAsync(classId);
            if (classModel == null)
            {
                throw new KeyNotFoundException("Classe nao encontrada.");
            }
            
            classModel.UpdateAllGradesAndFrequencies();
            await _context.SaveChangesAsync();
            await _unitRepository.UpdateAVGGradeFrequency_byUnitId(classModel.UnitId);

            return classModel;
        }

        public async Task<ClassSubjectModel> UpdateAVGGradeFrequency_byClassSubjectId(int classId, int subjectId)
        {
            var classSubjectModel = await _context.ClassSubjects.FindAsync(classId, subjectId);
            if (classSubjectModel == null)
            {
                throw new KeyNotFoundException("ClassSubject nao encontrada.");
            }
            
            classSubjectModel.UpdateAllGradesAndFrequencies();
            await _context.SaveChangesAsync();
            await UpdateAVGGradeFrequency_byClassId(classSubjectModel.ClassId);
            return classSubjectModel;
        }

        private (ClassModel, int) PrepareClass(ClassDTO new_class, int coordinatorId)
        {
            var coordinator =  _context.Coordinators.Find(coordinatorId);
            var unit =  _context.Units.Find(coordinator.UnitId);

            var newClass = new ClassModel
            {
                Name = new_class.Name,
                CoordinatorId = coordinatorId,
                UnitId = unit.Id,
                Grade = new_class.Serie
            };

            return (newClass, unit.Id);
        }
        private List<ClassSubjectModel> PrepareClassSubjects(ClassModel newClass, int unitId, int teacherId)
        {
            var subjects = _context.Subjects.Where(s => s.Grade == newClass.Grade).ToList();
            var classId = newClass.Id;
            
            var ClassSubjects = new List<ClassSubjectModel>();

            foreach (var subject in subjects)
            {
                ClassSubjects.Add( new ClassSubjectModel
                {
                    ClassId = newClass.Id,
                    UnitId = unitId,
                    TeacherId = teacherId,
                    SubjectId = subject.Id,
                });
            }

            return ClassSubjects;
        }

    
    }
}