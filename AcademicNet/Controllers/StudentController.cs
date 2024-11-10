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

        [HttpGet("ranking")]
        public async Task<ICollection<RankingDTO>> GetRanking_perStudent([FromQuery] int? unitId, [FromQuery] int? classId, [FromQuery] int? subjectId)
        {
            //Vou fazer os filtros de maneira iterada 
            //o filtro de classe acompanha o de unidade, já que não é possivel filtrar classe sem unidade
            //já o filtro de unidade não precisa ter um filtro de classe

            IQueryable<ClassSubjectModel> query = _context.ClassSubjects;
            ICollection<RankingDTO> ranking;

            if(subjectId != null)
            {
                //retorna todas as classes que fazem a materia X
                query = query.Where(cs => cs.SubjectId == subjectId);    
            }
            
            if (unitId != null)
            {
                //retorna uma iqueryable que contem todas as class_subject de uma disciplina X em uma unidade Y
                query = query.Where(cs => cs.UnitId == unitId);
                if (classId != null)
                {
                    //retorna uma iqueryable que contem a class_subject de uma disciplina X em uma unidade Y e classe Z
                    query = query.Where(cs => cs.ClassId == classId);
                } 
            }
            else
            {
                
            }

            IQueryable<StudentSubjectModel> intermediaryQuery = query.Include(cs => cs.StudentSubjects)
            .SelectMany(cs => cs.StudentSubjects);

            var orderedQuery = intermediaryQuery.OrderByDescending(ss => (float)ss.Grade * ss.Frequency);

            if(subjectId == null)
            {
                ranking = await orderedQuery
                .Include(ss => ss.Student.Class)  // Carrega a classe associada antes do agrupamento
                .GroupBy(ss => ss.Student)  // Agrupa por estudante
                .OrderByDescending(g => g.Average(g => g.Grade) * (decimal)g.Average(g => g.Frequency))
                .Select(g => new RankingDTO
                {
                    StudentName = g.Key.Name,
                    GradeFrequency = g.Average(ss => ss.Grade) * (decimal)g.Average(ss => ss.Frequency),
                    ClassName = g.Key.Class.Name, // Agora a classe já está carregada
                    
                })
                .Take(100)
                .ToListAsync();
            }

            ranking = await orderedQuery
            .Select(ss => new RankingDTO
            {
                StudentName = ss.Student.Name,
                ClassName = ss.ClassSubject.Class.Name,
                GradeFrequency = ss.Grade * (decimal)ss.Frequency,
                UnitName = unitId != null ? null : ss.Student.Class.Unit.Name //carrega o nome da unidade se não houver filtro de unidade
            })
            .Take(100)
            .ToListAsync();

            return ranking;            
        }

        [HttpGet("rankingAVG")]
        public async Task<ICollection<RankingDTO>> GetRanking_perUnit_Class([FromQuery] int? unitId, [FromQuery] int? subjectId)
        {
            IQueryable<ClassSubjectModel> query = _context.ClassSubjects;
            ICollection<RankingDTO> ranking;

            //se subjectId for nulo teremos um ranking que relaciona as unidades levando em conta as medias de todas as turmas em todas as disciplinas de todas as unidades
            if(subjectId != null)
            {
                query = query.Where(cs => cs.SubjectId == subjectId);
            }

            
            if(unitId == null)
            {
                ranking = await query.GroupBy(cs => cs.Unit)
                .OrderByDescending(g => g.SelectMany(cs => cs.StudentSubjects).Average(ss => ss.Grade) *
                    (decimal)g.SelectMany(cs => cs.StudentSubjects).Average(ss => ss.Frequency))
                .Select(g => new RankingDTO
                {
                    UnitName = g.Key.Name,
                    GradeFrequency = g.SelectMany(cs => cs.StudentSubjects).Average(ss => ss.Grade) *
                    (decimal)g.SelectMany(cs => cs.StudentSubjects).Average(ss => ss.Frequency),
                    //GradeFrequency = g.Average(cs => cs.StudentSubjects.Average(ss => ss.Grade) * 
                    //(decimal)g.Average(cs => cs.StudentSubjects.Average(ss => ss.Frequency))),
                })
                .Take(10)
                .ToListAsync();

                return ranking;
            }

            //se unitId é diferente de nulo teremos um ranking que relaciona as salas
            if(unitId != null)
            {
                ranking = await query.Where(cs => cs.UnitId == unitId)
                .GroupBy(cs => cs.Class)
                .OrderByDescending(g => g.SelectMany(cs => cs.StudentSubjects).Average(ss => ss.Grade) *
                    (decimal)g.SelectMany(cs => cs.StudentSubjects).Average(ss => ss.Frequency))
                .Select(g => new RankingDTO
                {
                    ClassName = g.Key.Name,
                    GradeFrequency = g.SelectMany(cs => cs.StudentSubjects).Average(ss => ss.Grade) *
                    (decimal)g.SelectMany(cs => cs.StudentSubjects).Average(ss => ss.Frequency),
                })
                .Take(20)
                .ToListAsync();

                return ranking;
            }

            return null;


        }
    }
}