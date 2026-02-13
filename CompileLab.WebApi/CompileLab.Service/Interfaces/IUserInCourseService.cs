using CompileLab.Repository.Entities;
using CompileLab.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Interfaces
{
    public interface IUserInCourseService
    {
        Task<List<UserInCourseDto>> GetAll();
        Task<UserInCourseDto> GetById(int id);
        Task<UserInCourseDto> AddItem(UserInCourseDto item, int userId);
        Task<UserInCourseDto> UpdateItem(int id, CourseStatus status, int userId);
        Task DeleteItem(int id, int userId);
        

    }
}
