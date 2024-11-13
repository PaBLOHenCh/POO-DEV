using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Data;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudiesGroupController : ControllerBase
    {
        private readonly AcademicNetDbContext _context;

        public StudiesGroupController(AcademicNetDbContext context)
        {
            _context = context;
        }

        [HttpGet("loadStudiesGroup", Name = "GetLeagueByStudentId")]
        public async Task<ICollection<StudentStudiesGroupModel>> LoadStudiesGroups_byStudent([FromQuery] int? studentId)
        {
            //ligas em que o estudante esta inserido
            return await _context.StudentStudiesGroups.Where(ssg => ssg.StudentId == studentId).ToListAsync();
        }
        [HttpGet("loadPostages")]
        public async Task<ICollection<PostageModel>> LoadPostages_byStudiesGroup([FromQuery] int? studiesGroupId, [FromQuery] int? page = 0)
        {
            
            //carregas as postagens em uma certa liga, por ordem de data
            //introduz a paginação das postagens de uma liga
            return await _context.Postages.Where(p => p.StudentStudiesGroupStudiesGroupId == studiesGroupId).Where(p => p.ParentPostageId == null)
                    .OrderBy(p => p.CreationDate).Skip((int)page* 10).Take(10).ToListAsync();
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