using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcademicNet.Models;
using Microsoft.EntityFrameworkCore;
using AcademicNet.Data;



namespace AcademicNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AcademicNetDbContext _context;

        public StudentController(AcademicNetDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<StudentModel>> CreateStudent(StudentModel student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentModel>> GetStudentById(int id)
        {
            var student = await _context.Students.Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == id);
            if(student == null)
            {
                return NotFound();//404
            }
            var matriculations = await _context.Matriculations.Where(m => m.StudentId == id).ToListAsync();

            student.CalculateAverageFrequency(matriculations);
            student.CalculateAverageGrade(matriculations);
            return Ok(student);//200
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();//204
        }

    }
}