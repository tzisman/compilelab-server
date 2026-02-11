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
    public class TestCaseRepository(IContext context) : IRepository<TestCase>
    {
        private readonly IContext _ctx = context;
        public async Task<TestCase> AddItem(TestCase item)
        {
            await _ctx.TestCases.AddAsync(item);
            await _ctx.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var testCase = _ctx.TestCases.FirstOrDefault(x => x.Id == id);
            if (testCase != null)
            {
                _ctx.TestCases.Remove(testCase);
                await _ctx.Save();
            }
        }

        public async Task<List<TestCase>> GetAll()
        {
            return await _ctx.TestCases
                .Include(tc => tc.Exercise)             
                    .ThenInclude(ex => ex.Course)       
                        .ThenInclude(c => c.Lecturer)   
                .ToListAsync();
        }

        public async Task<TestCase> GetById(int id)
        {
            var testCase = await _ctx.TestCases
            .Include(tc => tc.Exercise)           
            .ThenInclude(ex => ex.Course)       
            .ThenInclude(c => c.Lecturer)   
            .FirstOrDefaultAsync(x => x.Id == id);
            if (testCase == null)
            {
                return null;
            }
            return testCase;
        }

        public async Task<TestCase> UpdateItem(int id, TestCase item)
        {
            var testCase = await _ctx.TestCases
            .Include(tc => tc.Exercise)
            .ThenInclude(ex => ex.Course)
            .ThenInclude(c => c.Lecturer)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (testCase == null)
            {
                return null;
            }
            testCase.Output = item.Output;
            testCase.Input = item.Input;
            await _ctx.Save();
            return testCase;
        }
    }
}
