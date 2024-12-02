using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Data;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcademicNet.Interfaces;
using AcademicNet.DTO;

namespace AcademicNet.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class StudiesGroupController : ControllerBase
    {
        private readonly IStudiesGroupService _studiesGroupService;

        public StudiesGroupController(IStudiesGroupService studiesGroupService)
        {
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
        public async Task<ActionResult<IEnumerable<PostageDTO>>> LoadPostages_byStudiesGroup([FromQuery] int? studentId, [FromQuery] int? studiesGroupId, [FromQuery] int? page)
        {
            
            //carregas as postagens em uma certa liga, por ordem de data
            //introduz a paginação das postagens de uma liga
            try
            {
                return Ok( await _studiesGroupService.LoadPostages_byStudiesGroup(studentId, studiesGroupId, page));
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpGet("loadReplies")]
        public async Task<ActionResult<IEnumerable<PostageDTO>>> LoadReplies_byPostage([FromQuery] int? postageId, [FromQuery] int? page)
        {
            try
            {
                return Ok(await _studiesGroupService.LoadReplies_byPostage(postageId, page));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("createPostage")]
        public async Task<IActionResult> CreatePostage([FromQuery]int studentId, [FromQuery]int studiesGroupId, [FromQuery]string textBody,
        [FromQuery]string? pathToPhoto, [FromQuery]int? parentPostageId)
        {
            try
            {
                var postage = await _studiesGroupService.CreatePostage(studentId, studiesGroupId, textBody, pathToPhoto, parentPostageId);
                return Ok(postage);
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("enterStudiesGroup")]
        public async Task<ActionResult> EnterStudiesGroup([FromQuery]int studentId, [FromQuery]int studiesGroupId)
        {
            try
            {
                await _studiesGroupService.EnterStudiesGroup(studentId, studiesGroupId);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


    }
}