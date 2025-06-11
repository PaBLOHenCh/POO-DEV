using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcademicNet.Models;
using AcademicNet.DTO;
using Microsoft.EntityFrameworkCore;
using AcademicNet.Data;
using AcademicNet.Interfaces;



namespace AcademicNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IStudiesGroupService _studiesGroupService;

        public StudentController(IStudentService studentService, IStudiesGroupService studiesGroupService)
        {
            _studentService = studentService;
            _studiesGroupService = studiesGroupService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateStudent([FromBody]StudentCreateDTO student)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    StudentModel newStudent = await _studentService.AddStudent(student);
                    return Ok();
                }
                catch (ArgumentException e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest(ModelState);
        }
   

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentModel>> GetStudentById(int id)
        {
            try
            {
                return await _studentService.GetStudentById(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudentById(id);
                return NoContent();//204
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpGet("ranking")]
        public async Task<ActionResult<IEnumerable<RankingDTO>>> GetRanking_perStudent([FromQuery] int? unitId, [FromQuery] int? classId, [FromQuery] int? subjectId)
        {
            try
            {
                return Ok(await _studentService.GetRanking_perStudent(unitId, classId, subjectId));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }         
        }
    


        [HttpGet("rankingClasses")]
        public async Task<ActionResult<IEnumerable<RankingDTO>>> GetRanking_perUnit_Class([FromQuery] int? unitId, [FromQuery] int? subjectId)
        {
            try
            {
                return Ok(await _studentService.GetRanking_per_Class(unitId, null, subjectId));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("rankingUnits")]
        public async Task<ActionResult<IEnumerable<RankingDTO>>> GetRanking_perUnit([FromQuery] int? subjectId)
        {
            try
            {
                return Ok(await _studentService.GetRanking_perUnit(subjectId));
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("createStudiesGroup")]
        public async Task<IActionResult> CreateStudiesGroup([FromQuery]int studentId, [FromQuery] string name, [FromQuery] string description)
        {
            try
            {
                await _studiesGroupService.CreateStudiesGroup(studentId, name, description);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    
        [HttpPost("matriculation")]
        public async Task<ActionResult<MatriculationDTO>> Matriculation ([FromQuery]int? studentId, [FromQuery]int? subjectId, [FromQuery]int? classId)
        {
            try
            {
                return Ok(await _studentService.Matriculation(studentId, subjectId, classId));
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
