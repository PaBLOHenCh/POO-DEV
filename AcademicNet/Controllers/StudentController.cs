using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AcademicNet.Models;
using AcademicNet.DTO;
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
        public async Task<ActionResult<StudentModel>> CreateStudent([FromQuery]StudentCreateDTO student)
        {
            var newStudent = new StudentModel{
                Name = student.Name, 
                Email = student.Email, 
                Password = student.Password, 
                Address = new AddressModel{
                    Logradouro = student.Address.Logradouro,
                    Numero = student.Address.Numero,
                    Complemento = student.Address.Complemento,
                    CEP = student.Address.CEP,
                    Cidade = student.Address.Cidade,
                    Bairro = student.Address.Bairro,
                    Estado = student.Address.Estado,
                    Referencia = student.Address.Referencia,
                    Selecionado = student.Address.Selecionado
                }, 
                Phone = student.Phone, 
                CPF = student.CPF, 
                Role = IdentityRole.Student,
                };
            
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id });
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

        [HttpPost("createStudiesGroup")]
        public async Task<IActionResult> CreateStudiesGroup([FromQuery]int studentId, [FromQuery] string name, [FromQuery] string description)
        {
            //cria um objeto que será a liga de estudos
            StudiesGroupModel newLeague = new StudiesGroupModel {
                Name = name,
                Description = description
            };

            //se o modelo de liga de estudos for valido 
            if(ModelState.IsValid)
            {
                //adiciona a liga de estudos ao banco
                _context.StudiesGroups.Add(newLeague);
                await _context.SaveChangesAsync();

                //busca o estudante que criou a liga
                StudentModel CreatorStudent = await _context.Students.FindAsync(studentId);
                if(CreatorStudent != null)
                {
                    //adicionando a matricula do aluno que criou a liga na tabela de StudentStudiesGroup
                    _context.StudentStudiesGroups.Add(new StudentStudiesGroupModel
                    {
                        Student = CreatorStudent,
                        StudiesGroup = newLeague,
                        StudentId = studentId,
                        StudiesGroupId = newLeague.Id
                    });
                    await _context.SaveChangesAsync();
                    return CreatedAtRoute("GetLeagueByStudentId", new{studentId = studentId});
                }
                return BadRequest(new {Message = "Estudante Criador não encontrado"});
            }
            return BadRequest(new {Message = "Erro ao criar Liga de Estudos"});
        }
    }
}