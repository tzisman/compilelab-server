using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Interfaces
{
    public interface IService<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> AddItem(T item);
        Task<T> UpdateItem(int id, T item);
        Task DeleteItem(int id);
        Task<T> AddItem(T item, int userId);
        Task<T> UpdateItem(int id, T item, int userId);
        Task DeleteItem(int id, int userId);
    }
}
