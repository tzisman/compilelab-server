using CheckBox.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserByEmail(string email);
    }
}
