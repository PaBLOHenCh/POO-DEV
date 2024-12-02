using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Interfaces;
using AcademicNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicNet.Services
{
    public class StudiesGroupService : IStudiesGroupService
    {
        private readonly IStudiesGroupRepository _studiesGroupRepository;
        private readonly IStudentRepository _studentRepository;

        public StudiesGroupService(IStudiesGroupRepository studiesGroupRepository, IStudentRepository studentRepository)
        {
            _studiesGroupRepository = studiesGroupRepository;
            _studentRepository = studentRepository;
        }
        public async Task<StudiesGroupModel> CreateStudiesGroup(int studentId, string name, string description)
        {
            //cria um objeto que será a liga de estudos
            StudiesGroupModel newLeague = new StudiesGroupModel {
                Name = name,
                Description = description
            };
            if(newLeague != null)
            {
                await _studiesGroupRepository.CreateStudiesGroup(newLeague);
                var creatorStudent = await _studentRepository.GetStudentById(studentId);
                if(creatorStudent != null)
                {
                    //adiciona o estudante criador da liga de estudos
                    await _studiesGroupRepository.AddStudentToStudiesGroup(creatorStudent, newLeague);
                    return newLeague;
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

        public async Task<IEnumerable<PostageDTO>> LoadPostages_byStudiesGroup(int? studentId, int? studiesGroupId, int? page)
        {
            if (page == null)
            {
                page = 0;
            }
            if(studentId == null || studiesGroupId == null)
            {
                throw new KeyNotFoundException("O estudante ou a liga de estudos nao foram fornecidos.");
            }
            else
            {
                var postages = await _studiesGroupRepository.LoadPostages_byStudiesGroup(studentId.Value, studiesGroupId.Value, page.Value);
                return postages;
            }
        }

        public async Task<IEnumerable<PostageDTO>> LoadReplies_byPostage(int ? postageId, int? page)
        {
            if (page == null)
            {
                page = 0;
            }
            if (postageId == null)
            {
                throw new ArgumentException("Postagem nao foi fornecida.");
            }
            else
            {
                var replies = await _studiesGroupRepository.LoadReplies_byPostage(postageId.Value, page.Value);
                return replies;
            }
        }
        
        public async Task<PostageDTO> CreatePostage(int studentId, int studiesGroupId, string textBody, string? pathToPhoto, int? parentPostageId )
        {
            if (await _studentRepository.GetStudentById(studentId) == null)
            {
                throw new KeyNotFoundException($"Estudante com id {studentId} nao encontrado.");
            }

            if (await _studiesGroupRepository.GetStudiesGroupById(studiesGroupId) == null)
            {
                throw new KeyNotFoundException($"Liga de estudos com id {studiesGroupId} nao encontrada.");
            }
            if (parentPostageId != null)
            {
                return await _studiesGroupRepository.CreateReply(studentId, studiesGroupId, parentPostageId.Value, pathToPhoto, textBody);
            }
            else
            {
               return await _studiesGroupRepository.CreatePostage(studentId, studiesGroupId, textBody, pathToPhoto); 
            }
            
        }

        public async Task<StudentStudiesGroupModel> EnterStudiesGroup(int? studentId, int? studiesGroupId)
        {
            if(studentId == null || studiesGroupId == null)
            {
                throw new ArgumentException("Os Id's do estudante e da liga de estudos devem ser informados.");
            }
            //esperando que o estudante e a liga de estudos existam, se não lança exception
            await _studiesGroupRepository.GetStudiesGroupById(studiesGroupId.Value);
            await _studentRepository.GetStudentById(studentId.Value);


            return await _studiesGroupRepository.EnterStudiesGroup(studentId.Value, studiesGroupId.Value);
        }
    
    }
}