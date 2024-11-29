using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Interfaces;
using AcademicNet.Models;

namespace AcademicNet.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        
        public async Task<ClassDTO> CreateClass(ClassDTO new_class, int? coordinatorId, int? teacherId)
        {
            if(new_class == null)
            {
                throw new ArgumentException("Classe não pode ser nula");
            }
            if(coordinatorId == null || teacherId == null)
            {
                throw new ArgumentException("Coordenador e Professor não foram informados.");
            }
            return await _classRepository.CreateClass(new_class, coordinatorId.Value, teacherId.Value);
        }

        public async Task<ClassDTO> GetClassById(int? id)
        {
            if(id == null)
            {
                throw new ArgumentException("Id nao pode ser nulo.");
            }
            try
            {
                return await _classRepository.GetClassById(id.Value);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }
    
        public async Task<ClassDTO> DeleteClassById(int? id)
        {
            if(id == null)
            {
                throw new ArgumentException("Id nao pode ser nulo.");
            }
            try
            {
                return await _classRepository.DeleteClassById(id.Value);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

    }
}