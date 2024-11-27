using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Interfaces;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicNet.Services
{
    public class StudiesGroupService : ControllerBase, IStudiesGroupService
    {
        private readonly IStudiesGroupRepository _studiesGroupRepository;
        private readonly IStudentRepository _studentRepository;

        public StudiesGroupService(IStudiesGroupRepository studiesGroupRepository, IStudentRepository studentRepository)
        {
            _studiesGroupRepository = studiesGroupRepository;
            _studentRepository = studentRepository;
        }
        public async Task<IActionResult> CreateStudiesGroup(int studentId, string name, string description)
        {
            //cria um objeto que será a liga de estudos
            StudiesGroupModel newLeague = new StudiesGroupModel {
                Name = name,
                Description = description
            };
            if(ModelState.IsValid)
            {
                await _studiesGroupRepository.CreateStudiesGroup(newLeague);
                var creatorStudent = await _studentRepository.GetStudentById(studentId);
                if(creatorStudent != null)
                {
                    //adiciona o estudante criador da liga de estudos
                    await _studiesGroupRepository.AddStudentToStudiesGroup(creatorStudent, newLeague);
                    return Ok();
                }
                else
                {
                    throw new KeyNotFoundException($"Estudante com id {studentId} nao encontrado.");
                }
            }
            throw new ArgumentException("O modelo de liga de estudos nao eh valido.");
        }

        public async Task<IEnumerable<StudiesGroupModel>> LoadStudiesGroup_by_Student(int? studentId)
        {
            if(studentId == null)
            {
                throw new ArgumentException("O estudante nao foi fornecido.");
            }
            else
            {
                var StudiesGroups = await _studiesGroupRepository.LoadStudiesGroup_by_Student(studentId.Value);
                return StudiesGroups;

            }
        }

        public async Task<IEnumerable<PostageModel>> LoadPostages_byStudiesGroup(int? studentId, int? studiesGroupId, int? page)
        {
            if (page == null)
            {
                page = 0;
            }
            if(studentId == null || studiesGroupId == null)
            {
                throw new ArgumentException("O estudante ou a liga de estudos nao foram fornecidos.");
            }
            else
            {
                var postages = await _studiesGroupRepository.LoadPostages_byStudiesGroup(studentId.Value, studiesGroupId.Value, page.Value);
                return postages;
            }
        }
        
    }
}