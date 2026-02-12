using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Interfaces
{
    public interface IUserInCourseAuthorization : IAuthorization
    {
        Task<bool> IsInCourse(int exerciseId, int userInCourseId);

        Task<bool> IsAllowedToChange(int userInCourseId);
    }
}
