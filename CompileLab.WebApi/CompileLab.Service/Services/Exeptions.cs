using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Services
{
    public class ForbiddenAccessException(string message) : Exception(message)
    {
    }
}
