using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Data;
using AcademicNet.Interfaces;
using AcademicNet.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicNet.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly AcademicNetDbContext _context;

        public UnitRepository(AcademicNetDbContext context)
        {
            _context = context;
        }

        public async Task<UnitModel> UpdateAVGGradeFrequency_byUnitId(int unitId)
        {
            var unitModel = await _context.Units.Include(u => u.Classes).Include(u => u.ClassSubjects).FirstOrDefaultAsync(u => u.Id == unitId);
            if (unitModel == null)
            {
                throw new KeyNotFoundException("Unidade nao encontrada.");
            }   

            unitModel.UpdateAllGradesAndFrequencies();
            await _context.SaveChangesAsync();

            return unitModel;
        }
    }
}