using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Interfaces;
using AcademicNet.DTO;
using AcademicNet.Models;

namespace AcademicNet.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentModel> AddStudent(StudentCreateDTO student)
        {
            ValidateStudentData(student);

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

            return  await _studentRepository.AddStudent(newStudent);
        }

        public Task<StudentModel> GetStudentById(int id)
        {
            var student =  _studentRepository.GetStudentById(id) ?? throw new KeyNotFoundException($"Estudante com id {id} não encontrado.");
            return student;
        }

        public async Task<StudentModel> DeleteStudentById(int id)
        {
            var student =  _studentRepository.GetStudentById(id);
            if(student != null)
            {
                return await _studentRepository.DeleteStudentById(id);   
            }
            else
            {
                throw new KeyNotFoundException($"Estudante com id {id} não encontrado.");
            }
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_perStudent(int? unitId, int? classId, int? subjectId)
        {
            if(unitId == null && classId == null)
            {
                if(subjectId != null)
                {
                    //Ranking por disciplina universal
                    return  await _studentRepository.GetRanking_Student_perSubject(subjectId.Value);
                }
                else if(subjectId == null)
                {
                    //Ranking universal
                    var ranking = await _studentRepository.GetRanking_AVGStudentUniversal();   
                    return ranking;
                }
            }

            if (unitId != null)
            {
                if(classId != null)
                {
                    if(subjectId != null)
                    {
                        //Ranking por disciplina dentro de uma classe e uma unidade
                        return await _studentRepository.GetRanking_Student_perUnit_Class_Subject(unitId.Value, classId.Value, subjectId.Value);
                    }
                    else if(subjectId == null)
                    {
                        //Ranking sem disciplina especifica(nota média) dentro de uma classe e uma unidade
                        return await _studentRepository.GetRanking_Student_perUnit_Class(unitId.Value, classId.Value);
                    }
                }
                else if(classId == null)
                {
                    if(subjectId != null)
                    {
                        //Ranking por disciplina dentro de uma unidade
                        return  await _studentRepository.GetRanking_Student_perUnit_Subject(unitId.Value, subjectId.Value);
                    }
                    else if(subjectId == null)
                    {
                        //Ranking sem disciplina especifica(nota média) dentro de uma unidade
                        return  await _studentRepository.GetRanking_Student_perUnit(unitId.Value);
                    }
                }
            }

            //lança uma excessão se os filtros informados forem inconsistentes
            throw new ArgumentException("Os filtros informados não correspondem a um ranking existente.");
            
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_per_Class(int? unitId, int? classId, int? subjectId)
        {
            if(unitId == null && classId == null)
            {
                if(subjectId != null)
                {
                    //Ranking de classes por disciplina - universal
                    return  await _studentRepository.GetRanking_Class_perSubject_Universal(subjectId.Value);
                }
                else if(subjectId == null)
                {
                    //Ranking de classes - universal
                    return  await _studentRepository.GetRanking_Class_Universal();
                }
            }
            else if(unitId != null)
            {
                if(classId == null)
                {
                    if(subjectId != null)
                    {
                        //Ranking de classes por disciplina dentro de uma unidade
                        return  await _studentRepository.GetRanking_Unit_Class_perSubject(unitId.Value, subjectId.Value);
                    }
                    else if(subjectId == null)
                    {
                        //Ranking de classes sem disciplina especifica(nota média) dentro de uma unidade
                        return  await _studentRepository.GetRanking_Unit_Class_No_Subject(unitId.Value);
                    }
                }
                else if(classId != null)
                {
                    if(subjectId == null)
                    {
                        //Ranking de classes sem disciplina especifica(nota média) dentro de uma unidade e uma classe
                        return  await _studentRepository.GetRanking_Unit_Class(unitId.Value, classId.Value);
                    }
                }
            }

            //lança uma excessão se os filtros informados forem inconsistentes
            throw new ArgumentException("Os filtros informados não correspondem a um ranking existente.");
        }

        public async Task<IEnumerable<RankingDTO>> GetRanking_perUnit(int? subjectId)
        {
            if(subjectId == null)
            {
                //Ranking universal
                return  await _studentRepository.GetRanking_Unit_Universal();
            }
            else
            {
                //Ranking dentro de uma unidade
                return  await _studentRepository.GetRanking_Unit_perSubject(subjectId.Value);
            }
            throw new ArgumentException("Os rankings de unidades so existem em relação a disciplinas ou que sejam universais.");
        }

        private void ValidateStudentData(StudentCreateDTO student)
        {
            if (string.IsNullOrEmpty(student.Name))
            {
                throw new ArgumentException("O nome do aluno deve ser informado.");
            }
            if (string.IsNullOrEmpty(student.Email))
            {
                throw new ArgumentException("O email do aluno deve ser informado.");
            }
            if (string.IsNullOrEmpty(student.Password))
            {
                throw new ArgumentException("A senha do aluno deve ser informada.");
            }
            if (string.IsNullOrEmpty(student.CPF))
            {
                throw new ArgumentException("O CPF do aluno deve ser informado.");
            }
        }

    }
}