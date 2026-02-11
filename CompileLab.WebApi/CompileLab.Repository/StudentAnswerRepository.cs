using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository
{
    public class StudentAnswerRepository(IContext context) : IRepository<StudentAnswer>
    {
        private readonly IContext _ctx = context;

        public async Task<StudentAnswer> AddItem(StudentAnswer item)
        {
            await _ctx.StudentAnswers.AddAsync(item);
            await _ctx.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var answer = await _ctx.StudentAnswers.FirstOrDefaultAsync(x => x.Id == id);
            if (answer != null)
            {
                _ctx.StudentAnswers.Remove(answer);
                await _ctx.Save();
            }
        }

        public async Task<List<StudentAnswer>> GetAll()
        {
            return await _ctx.StudentAnswers
                .Include(a => a.Exercise)
                    .ThenInclude(ex => ex.Course)
                        .ThenInclude(c => c.Lecturer) 
                .Include(a => a.StudentInCourse)
                    .ThenInclude(uic => uic.Student) 
                .ToListAsync();
        }

        public async Task<StudentAnswer> GetById(int id)
        {
            var answer = await _ctx.StudentAnswers
                .Include(a => a.Exercise)
                    .ThenInclude(ex => ex.Course)
                        .ThenInclude(c => c.Lecturer)
                .Include(a => a.StudentInCourse)
                    .ThenInclude(uic => uic.Student)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (answer == null)
            {
                return null;
            }

            return answer;
        }

        public async Task<StudentAnswer> UpdateItem(int id, StudentAnswer item)
        {
            var existing = await _ctx.StudentAnswers.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return null;

            existing.AnswerCode = item.AnswerCode;
            existing.Mark = item.Mark;
            existing.Remark = item.Remark;
            existing.ExerciseId = item.ExerciseId;
            existing.UserInCourseId = item.UserInCourseId;

            await _ctx.Save();
            return await GetById(id);
        }
    }
}
