using CheckBox.Repository.Entities;
using CheckBox.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> UpdateItem(int id, UserDto item);
        Task DeleteItem(int id);
    }
}
