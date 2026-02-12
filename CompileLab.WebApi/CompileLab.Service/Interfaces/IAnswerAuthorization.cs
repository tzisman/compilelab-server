using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Interfaces
{
    public interface IAnswerAuthorization : IAuthorization
    {
        Task<bool> IsAllowedToChange(int answerId);
    }
}
