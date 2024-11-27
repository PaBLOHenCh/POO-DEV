using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Data;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademicNet.Interfaces;

namespace AcademicNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudiesGroupController : ControllerBase
    {
        private readonly AcademicNetDbContext _context;
        private readonly IStudiesGroupService _studiesGroupService;

        public StudiesGroupController(AcademicNetDbContext context, IStudiesGroupService studiesGroupService)
        {
            _context = context;
            _studiesGroupService = studiesGroupService;
        }

        [HttpGet("loadStudiesGroup", Name = "GetLeagueByStudentId")]
        public async Task<ActionResult<IEnumerable<StudiesGroupModel>>> LoadStudiesGroups_byStudent([FromQuery] int studentId)
        {
            try
            {
                return Ok(await _studiesGroupService.LoadStudiesGroup_by_Student(studentId));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            
        }
        [HttpGet("loadPostages")]
        public async Task<ActionResult<IEnumerable<PostageModel>>> LoadPostages_byStudiesGroup([FromQuery] int? studentId, [FromQuery] int? studiesGroupId, [FromQuery] int? page)
        {
            
            //carregas as postagens em uma certa liga, por ordem de data
            //introduz a paginação das postagens de uma liga
            try
            {
                return Ok(await _studiesGroupService.LoadPostages_byStudiesGroup(studentId, studiesGroupId, page));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("loadReplies")]
        public async Task<ICollection<PostageModel>> LoadReplies_byPostage([FromQuery] int? postageId, [FromQuery] int? page = 0)
        {
            return await _context.Postages.Where(p => p.Id == postageId).SelectMany(p => p.Replies).OrderBy(p => p.CreationDate).Skip((int)page * 10).Take(10).ToListAsync();
        }

        [HttpPost("createPostage")]
        public async Task<IActionResult> CreatePostage([FromQuery]int studentId, [FromQuery]int studiesGroupId, [FromQuery]string textBody,
         [FromQuery]string? pathToPhoto, [FromQuery]int? parentPostageId = null)
        {
            // Verificar se o grupo de estudos e o estudante existem no banco de dados
            var studiesGroup = await _context.StudiesGroups.FindAsync(studiesGroupId);
            var student = await _context.Students.FindAsync(studentId);
            var studentStudiesGroup = await _context.StudentStudiesGroups.FirstOrDefaultAsync(ssg => ssg.StudiesGroupId == studiesGroupId && ssg.StudentId == studentId);

            if (studiesGroup == null)
            {
                return BadRequest(new { Message = "Grupo de estudos não encontrado." });
            }
            if (student == null)
            {
                return BadRequest(new { Message = "Estudante não encontrado." });
            }
            if(studentStudiesGroup == null)
            {
                return BadRequest(new { Message = "Estudante não pertence ao grupo de estudos." });
            }

            // Se parentPostageId for fornecido, verifica se a postagem pai existe
            PostageModel? parentPostage = null;
            if (parentPostageId.HasValue)
            {
                parentPostage = await _context.Postages.FindAsync(parentPostageId.Value);
                if (parentPostage == null)
                {
                    return BadRequest(new { Message = "Postagem pai não encontrada." });
                }
            }

            var postage = new PostageModel{
                StudentStudiesGroupStudiesGroupId = studiesGroupId,
                StudentStudiesGroupStudentId = studentId,
                ParentPostageId = parentPostageId,
                TextBody = textBody,
                PathToPhoto = pathToPhoto,
                ParentPostage = parentPostage,
                CreationDate = DateTime.UtcNow.AddHours(-3)
            };
            if(ModelState.IsValid)
            {
                _context.Postages.Add(postage);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Postagem criada com sucesso." });
            }
            return BadRequest();
        }
    }
}