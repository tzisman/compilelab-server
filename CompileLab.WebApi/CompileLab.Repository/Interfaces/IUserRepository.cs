using CompileLab.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserByEmail(string email);

        Task<List<Course>> GetCourseOfUser(int id);
        Task<List<Course>> GetCourseOfLecturer(int id);
        Task<List<UserInCourse>> GetReqwestOfUser(int id);


    }
}
