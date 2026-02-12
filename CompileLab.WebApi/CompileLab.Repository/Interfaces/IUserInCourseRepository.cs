using CompileLab.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Interfaces
{
    public interface IUserInCourseRepository : IRepository<UserInCourse>
    {
        public Task<UserInCourse?> GetByUserAndCourse(int courseId, int userId);
    }
}
