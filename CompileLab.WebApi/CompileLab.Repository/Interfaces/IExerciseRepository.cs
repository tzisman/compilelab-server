using CompileLab.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Interfaces
{
    public interface IExerciseRepository : IRepository<CodeExercise>
    {
            public Task<List<CodeExercise>> GetExercisesByCourseId(int courseId);
    }
}
