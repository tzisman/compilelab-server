using CompileLab.Repository.Entities;
using Microsoft.EntityFrameworkCore;
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
        public Task<List<CodeExercise>> GetExercisesWithGradesByCourse(int courseId, int userId);

        public Task<CodeExercise?> GetExerciseWithStudentAnswer(int exerciseId, int userId);
    }
}
