using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Interfaces;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoordinatorController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;

        public CoordinatorController(IClassService classService, ISubjectService subjectService)
        {
            _classService = classService;
            _subjectService = subjectService;
        }

        [HttpPost("createClass")]
        public async Task<ActionResult<ClassDTO>> CreateClass([FromQuery]ClassDTO new_class, [FromQuery]int? coordinatorId, [FromQuery]int? teacherId)
        {
            try
            {
                return Ok(await _classService.CreateClass(new_class, coordinatorId, teacherId));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("getClassById")]
        public async Task<ActionResult<ClassDTO>> GetClassById(int? id)
        {
            try
            {
                return Ok(await _classService.GetClassById(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("deleteClassById")]
        public async Task<ActionResult<ClassDTO>> DeleteClassById(int? id)
        {
            try
            {
                return Ok(await _classService.DeleteClassById(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("addSubject")]
        public async Task<ActionResult<SubjectDTO>> AddSubject([FromQuery]SubjectDTO new_subject)
        {
            try
            {
                return Ok(await _subjectService.AddSubject(new_subject));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("getSubjectById")]
        public async Task<ActionResult<SubjectDTO>> GetSubjectById(int id)
        {
            try
            {
                return Ok(await _subjectService.GetSubjectById(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("deleteSubjectById")]
        public async Task<ActionResult<SubjectDTO>> DeleteSubjectById(int id)
        {
            try
            {
                return Ok(await _subjectService.DeleteSubjectById(id));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}