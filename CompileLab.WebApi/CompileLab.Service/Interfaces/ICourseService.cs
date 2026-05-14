using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseDisplayDto>> GetAll(int page, int size, string search);
        Task<CourseDisplayDto> GetById(int id);
        Task<CourseDto> AddItem(CourseDto item, int userId);
        Task<CourseDto> UpdateItem(int id, CourseDto item, int userId);
        Task DeleteItem(int id, int userId);
    }
}
