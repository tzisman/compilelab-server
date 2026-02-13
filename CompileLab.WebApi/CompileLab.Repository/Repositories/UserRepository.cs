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
    public class UserRepository(IContext context) : IUserRepository
    {
        private readonly IContext _ctx = context;

        public async Task<User> AddItem(User item)
        {
            await _ctx.Users.AddAsync(item);
            await _ctx.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var user = _ctx.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {         
                _ctx.Users.Remove(user);
                await _ctx.Save();
            }
        }

        public async Task<List<User>> GetAll()
        {
            return await _ctx.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null; 
            }
            return user;
        }

        public async Task<List<Course>> GetCourseOfLecturer(int id)
        {
            var courses = await _ctx.Courses
            .Where(uc => uc.LecturerId == id)
            .ToListAsync();

            return courses;
        }

        public async Task<List<Course>> GetCourseOfUser(int id)
        {
            var courses = await _ctx.UserInCourses
            .Where(uc => uc.UserId == id)
            .Select(uc => uc.Course)
            .ToListAsync();

            return courses;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User> UpdateItem(int id, User item)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            user.Email = item.Email;
            user.Name = item.Name;
            await _ctx.Save();
            return user;
        }
    }
}
