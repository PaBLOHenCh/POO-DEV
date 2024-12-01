using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicNet.Models;

namespace AcademicNet.Interfaces
{
    public interface IUnitRepository
    {
        public Task<UnitModel> UpdateAVGGradeFrequency_byUnitId(int unitId);

    }
}