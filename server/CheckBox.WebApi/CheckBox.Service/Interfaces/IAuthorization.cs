using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Interfaces
{
    public interface IAuthorization
    {
        Task<bool> IsOwnerOf(int targetId, int userId);
    }
}
