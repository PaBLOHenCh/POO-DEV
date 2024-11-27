using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Data;
using AcademicNet.Interfaces;
using AcademicNet.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace AcademicNet.Repositories
{
    public class StudiesGroupRepository : ControllerBase, IStudiesGroupRepository
    {
        private readonly AcademicNetDbContext _context;

        public StudiesGroupRepository(AcademicNetDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateStudiesGroup(StudiesGroupModel studiesGroup)
        {
            _context.StudiesGroups.Add(studiesGroup);
            await _context.SaveChangesAsync();
            return Ok();
        }
        
        public async Task<IActionResult> AddStudentToStudiesGroup(StudentModel? student, StudiesGroupModel? studiesGroup)
        {
            _context.StudentStudiesGroups.Add(new StudentStudiesGroupModel
            {
                Student = student,
                StudiesGroup = studiesGroup,
                StudentId = student.Id,
                StudiesGroupId = studiesGroup.Id
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        public async Task<IEnumerable<StudiesGroupModel>> LoadStudiesGroup_by_Student(int studentId)
        {
            return await _context.StudiesGroups
            .Where(sg => sg.StudentStudiesGroups.Any(ssg => ssg.StudentId == studentId))
            .ToListAsync();
        }

        public async Task<IEnumerable<PostageModel>> LoadPostages_byStudiesGroup(int studentId, int studiesGroupId, int page)
        {
            return await _context.Postages
            .Where(p => p.StudentStudiesGroupStudiesGroupId == studiesGroupId)
            .Where(p => p.StudentStudiesGroupStudentId == studentId)
            .Where(p => p.ParentPostageId == null)
            .OrderBy(p => p.CreationDate)
            .Skip(page * 10)
            .Take(10)
            .ToListAsync();
            
        }
    }
}