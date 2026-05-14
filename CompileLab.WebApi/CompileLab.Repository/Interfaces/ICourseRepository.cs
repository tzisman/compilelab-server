using CompileLab.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course> GetById(int id);
        Task<List<Course>> GetAll(int pageNumber = 1, int pageSize = 10, string? searchTerm = null);
        Task<Course> AddItem(Course item);
        Task<Course> UpdateItem(int id, Course item);
        Task DeleteItem(int id);
    }
}
