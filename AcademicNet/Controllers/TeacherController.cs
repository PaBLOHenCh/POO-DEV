using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Interfaces;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }


        [HttpGet("getClassSubject_byTeacherId")]
        public async Task<ActionResult<IEnumerable<ClassSubjectModel>>> GetClassSubject_byTeacherId(int? id)
        {
            try
            {
                return Ok(await _teacherService.GetClassSubject_byTeacherId(id));
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
        [HttpGet("getMatriculations_byClassSubjectId")]
        public async Task<ActionResult<IEnumerable<StudentSubjectModel>>> GetMatriculations_byClassSubjectId(int? classId, int? subjectId)
        {
            try
            {
                return Ok(await _teacherService.GetMatriculations_byClassSubjectId(classId, subjectId));
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

        [HttpPost("ThrowGrade")]
        public async Task<ActionResult> ThrowGrade([FromBody]int? studentId, [FromBody]int? subjectId, [FromBody]float? grade, [FromBody]float? frequency)
        {
            try
            {
                await _teacherService.ThrowGrade(studentId, subjectId, grade, frequency);
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

        [HttpPost("GaveSubject")]
        public async Task<ActionResult> GaveSubject([FromBody]int? teacherId, [FromBody]int? subjectId, [FromBody]int? classId)
        {
            try
            {
                await _teacherService.GaveSubject(teacherId, subjectId, classId);
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