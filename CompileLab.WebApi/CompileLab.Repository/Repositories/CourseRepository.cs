using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Repositories
{
    public class CourseRepository(IContext context) : ICourseRepository
    {
        private readonly IContext _ctx = context;
        public async Task<Course> AddItem(Course item)
        {
            await _ctx.Courses.AddAsync(item);
            await _ctx.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var course = _ctx.Courses.FirstOrDefault(x => x.Id == id);
            if (course != null)
            {        
                _ctx.Courses.Remove(course);
                await _ctx.Save();
            }
        }

        public async Task<List<Course>> GetAll(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
        {
            var query = _ctx.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Exercises)
                .Include(c => c.Studies)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Name.Contains(searchTerm));
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Course> GetById(int id)
        {
            var course = await _ctx.Courses.Include(c => c.Lecturer).Include(c => c.Exercises)
                .Include(c => c.Studies).FirstOrDefaultAsync(x => x.Id == id);
            return course;
        }

        public async Task<Course> UpdateItem(int id, Course item)
        {
            var course = await _ctx.Courses.Include(c => c.Lecturer).FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return null;
            }
            course.Name = item.Name;
            await _ctx.Save();
            return course;
        }
    }
}
