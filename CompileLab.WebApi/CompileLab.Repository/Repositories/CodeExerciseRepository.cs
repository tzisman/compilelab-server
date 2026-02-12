using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Repositories
{
    public class CodeExerciseRepository(IContext context) : IRepository<CodeExercise>
    {
        private readonly IContext _ctx = context;

        public async Task<CodeExercise> AddItem(CodeExercise item)
        {
            await _ctx.CodeExercises.AddAsync(item);
            await _ctx.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var exercise = await _ctx.CodeExercises.FirstOrDefaultAsync(x => x.Id == id);
            if (exercise != null)
            {
                _ctx.CodeExercises.Remove(exercise);
                await _ctx.Save();
            }
        }

        public async Task<List<CodeExercise>> GetAll()
        {
             return await _ctx.CodeExercises
                .Include(ex => ex.Course)
                    .ThenInclude(c => c.Lecturer) 
                .Include(ex => ex.EdgeCases)   
                .ToListAsync();
        }

        public async Task<CodeExercise> GetById(int id)
        {
            var exercise = await _ctx.CodeExercises
                .Include(ex => ex.Course)
                    .ThenInclude(c => c.Lecturer)
                .Include(ex => ex.EdgeCases)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (exercise == null)
            {
                return null;
            }

            return exercise;
        }

        public async Task<CodeExercise> UpdateItem(int id, CodeExercise item)
        {
            var existing = await _ctx.CodeExercises.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return null;

            existing.ExerciseName = item.ExerciseName;
            existing.Description = item.Description;
            existing.Language = item.Language;

            await _ctx.Save();
            return await GetById(id); 
        }
    }
}
