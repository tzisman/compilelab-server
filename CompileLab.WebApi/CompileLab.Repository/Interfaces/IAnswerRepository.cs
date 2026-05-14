using CompileLab.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Repository.Interfaces
{
    public interface IAnswerRepository: IRepository<StudentAnswer>
    {
        public Task<StudentAnswer> UpdateMark(int answerId, double mark);
    }
}
