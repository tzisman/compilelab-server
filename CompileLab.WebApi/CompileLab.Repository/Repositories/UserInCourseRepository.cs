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
    public class UserInCourseRepository(IContext context) : IUserInCourseRepository
    {
        private readonly IContext _ctx = context;

        public async Task<UserInCourse> AddItem(UserInCourse item)
        {
            item.Status = CourseStatus.sent;
            //item.Status = CourseStatus.Approved;
            await _ctx.UserInCourses.AddAsync(item);
            await _ctx.Save();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var uic = _ctx.UserInCourses.FirstOrDefault(x => x.Id == id);
            if (uic != null)
            {
                _ctx.UserInCourses.Remove(uic);
                await _ctx.Save();
            }
        }

        public async Task<List<UserInCourse>> GetAll()
        {
            return await _ctx.UserInCourses
            .Include(uic => uic.Course)        
            .ThenInclude(c => c.Lecturer)      
            .Include(uic => uic.Student).ToListAsync();
        }

        public async Task<UserInCourse> GetById(int id)
        {
            var userInCourse = await _ctx.UserInCourses
            .Include(uic => uic.Course)            
            .ThenInclude(c => c.Lecturer)     
            .Include(uic => uic.Student)         
            .FirstOrDefaultAsync(x => x.Id == id);

            if (userInCourse == null)
            {
                return null;
            }

            return userInCourse;
        }

        public async Task<UserInCourse?> GetByUserAndCourse(int courseId, int userId)
        {
            var uic = await _ctx.UserInCourses
            .Include(uic => uic.Course)
            .ThenInclude(c => c.Lecturer)
            .Include(uic => uic.Student)
            .Where(uic => uic.CourseId == courseId && uic.UserId == userId).FirstOrDefaultAsync();
            return uic;
        }

        public async Task<UserInCourse> UpdateItem(int id, UserInCourse item)
        {
            var existingItem = await _ctx.UserInCourses.FirstOrDefaultAsync(x => x.Id == id);

            if (existingItem == null)
            {
                return null;
            }


            existingItem.Status = item.Status;

            await _ctx.Save();

            return existingItem;
        }
    }
}
