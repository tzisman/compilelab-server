using CompileLab.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Interfaces
{
    public interface IExerciseService : IService<CodeExerciseDto>
    {
        Task<List<CodeExerciseDto>> GetExercisesByCourseId(int courseId);
    }
}
