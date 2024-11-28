using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Data;
using AcademicNet.DTO;
using AcademicNet.Interfaces;
using AcademicNet.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AcademicNet.Repositories
{
    public class StudiesGroupRepository : ControllerBase, IStudiesGroupRepository
    {
        private readonly AcademicNetDbContext _context;

        public StudiesGroupRepository(AcademicNetDbContext context)
        {
            _context = context;
        }

        public async Task<StudiesGroupModel> GetStudiesGroupById(int id)
        {
            return await _context.StudiesGroups.FindAsync(id);
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
            var query = _context.StudentStudiesGroups
            .Where(ssg => ssg.StudentId == studentId)
            .Select(ssg => ssg.StudiesGroup);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<PostageDTO>> LoadPostages_byStudiesGroup(int studentId, int studiesGroupId, int page)
        {
            var student = await _context.Students.FindAsync(studentId);
            var studiesGroup = await _context.StudiesGroups.FindAsync(studiesGroupId);

            if (student == null || studiesGroup == null)
            {
                throw new KeyNotFoundException("O estudante ou a liga de estudos nao existem.");
            }

            var postages = await _context.Postages
            .Where(p => p.StudentStudiesGroupStudiesGroupId == studiesGroupId && p.StudentStudiesGroupStudentId == studentId && p.ParentPostageId == null)
            .OrderBy(p => p.CreationDate)
            .Skip(page * 10)
            .Take(10)
            .ToListAsync();

            var postagesDTO = postages.Select(p => new PostageDTO
            {
                StudentName = student.Name,
                CreationDate = p.CreationDate,
                TextBody = p.TextBody,
                PathToPhoto = p.PathToPhoto
            }).ToList();

            return postagesDTO;
        }

        public async Task<IEnumerable<PostageDTO>> LoadReplies_byPostage(int postageId, int page)
        {

            var replies = await _context.Postages
            .Where(p => p.Id == postageId)
            .SelectMany(p => p.Replies)
            .Include(p => p.StudentStudiesGroup)
            .ThenInclude(ssg => ssg.Student)
            .OrderBy(p => p.CreationDate)
            .Skip(page * 10)
            .Take(10)
            .ToListAsync();

            var repliesDTO = replies.Select(p => new PostageDTO
            {
                StudentName = p.StudentStudiesGroup.Student.Name,
                CreationDate = p.CreationDate,
                TextBody = p.TextBody,
                PathToPhoto = p.PathToPhoto
            }).ToList();

            return repliesDTO;
        }

        public async Task<PostageDTO> CreatePostage(int studentId, int studiesGroupId, string textBody, string? pathToPhoto)
        {
            var studentStudiesGroup = await _context.StudentStudiesGroups.FirstOrDefaultAsync
            (ssg => ssg.StudiesGroupId == studiesGroupId && ssg.StudentId == studentId);

            var postageDTO = new PostageDTO
            {
                StudentName = studentStudiesGroup.Student.Name,
                CreationDate = DateTime.UtcNow,
                TextBody = textBody,
                PathToPhoto = pathToPhoto
            };

            var postage = new PostageModel
            {
                CreationDate = DateTime.UtcNow,
                StudentStudiesGroup = studentStudiesGroup,
                StudentStudiesGroupStudiesGroupId = studiesGroupId,
                StudentStudiesGroupStudentId = studentId,
                TextBody = textBody,
                PathToPhoto = pathToPhoto
            };
            _context.Postages.Add(postage);
            await _context.SaveChangesAsync();
            return postageDTO;
        }

        public async Task<PostageDTO> CreateReply(int studentId, int studiesGroupId, int parentPostageId, string textBody, string? pathToPhoto)
        {
            var studentStudiesGroup = await _context.StudentStudiesGroups.FirstOrDefaultAsync(ssg => ssg.StudiesGroupId == studiesGroupId && ssg.StudentId == studentId);
            var parentPostage = await _context.Postages.FindAsync(parentPostageId);

            var postageDTO = new PostageDTO
            {
                StudentName = studentStudiesGroup.Student.Name,
                CreationDate = DateTime.UtcNow,
                TextBody = textBody,
                PathToPhoto = pathToPhoto
            };

            var postage = new PostageModel
            {
                CreationDate = DateTime.UtcNow,
                StudentStudiesGroup = studentStudiesGroup,
                StudentStudiesGroupStudiesGroupId = studiesGroupId,
                StudentStudiesGroupStudentId = studentId,
                ParentPostage = parentPostage,
                ParentPostageId = parentPostageId,
                TextBody = textBody,
                PathToPhoto = pathToPhoto
            };
            _context.Postages.Add(postage);
            await _context.SaveChangesAsync();

            return postageDTO;

        }
    }
}