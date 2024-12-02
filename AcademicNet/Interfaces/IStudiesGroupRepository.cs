using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicNet.Interfaces
{
    public interface IStudiesGroupRepository
    {
        public Task<StudiesGroupModel> GetStudiesGroupById(int id);
        public Task<IActionResult> CreateStudiesGroup(StudiesGroupModel studiesGroup);
        public Task<IActionResult> AddStudentToStudiesGroup(StudentModel? student, StudiesGroupModel? studiesGroup);
        public Task<IEnumerable<StudiesGroupModel>> LoadStudiesGroup_by_Student(int studentId);
        public Task<IEnumerable<PostageDTO>> LoadPostages_byStudiesGroup(int studentId, int studiesGroupId, int page);
        public Task<IEnumerable<PostageDTO>> LoadReplies_byPostage(int postageId, int page);
        public Task<PostageDTO> CreatePostage(int studentId, int studiesGroupId, string textBody, string? pathToPhoto);
        public Task<PostageDTO> CreateReply(int studentId, int studiesGroupId, int parentPostageId, string textBody, string? pathToPhoto);
        public Task<StudentStudiesGroupModel> EnterStudiesGroup(int studentId, int studiesGroupId);
    }
}