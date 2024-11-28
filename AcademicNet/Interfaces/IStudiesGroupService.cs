using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicNet.Interfaces
{
    public interface IStudiesGroupService
    {
        public Task<StudiesGroupModel> CreateStudiesGroup(int studentId, string name, string description);
        public Task<IEnumerable<StudiesGroupModel>> LoadStudiesGroup_by_Student(int? studentId);
        public Task<IEnumerable<PostageDTO>> LoadPostages_byStudiesGroup(int? studentId, int? studiesGroupId, int? page);
        public Task<IEnumerable<PostageDTO>> LoadReplies_byPostage(int ? postageId, int? page);
        public Task<PostageDTO> CreatePostage(int studentId, int studiesGroupId, string textBody, string? pathToPhoto, int? parentPostageId );

    }
}