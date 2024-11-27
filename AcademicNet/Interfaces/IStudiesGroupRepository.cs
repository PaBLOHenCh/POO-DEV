using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicNet.Interfaces
{
    public interface IStudiesGroupRepository
    {
        public Task<IActionResult> CreateStudiesGroup(StudiesGroupModel studiesGroup);
        public Task<IActionResult> AddStudentToStudiesGroup(StudentModel? student, StudiesGroupModel? studiesGroup);
        public Task<IEnumerable<StudiesGroupModel>> LoadStudiesGroup_by_Student(int studentId);
        public Task<IEnumerable<PostageModel>> LoadPostages_byStudiesGroup(int studentId, int studiesGroupId, int page);
    }
}