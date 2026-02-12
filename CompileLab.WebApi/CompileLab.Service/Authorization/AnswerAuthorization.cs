using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using CompileLab.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Authorization
{
    public class AnswerAuthorization(IRepository<StudentAnswer> repository) : IAnswerAuthorization
    {
       private readonly IRepository<StudentAnswer> _repository = repository;

        public async Task<bool> IsAllowedToChange(int answerId)
        {
            var result = await _repository.GetById(answerId);
            if (result == null) return false;
            
            return result.StudentInCourse.Status == CourseStatus.Approved;
        }

        public async Task<bool> IsOwnerOf(int targetId, int userId)
        {
            var result = await _repository.GetById(targetId);
            if (result == null)
                return false;

            return result.StudentInCourse.UserId == userId;
        }
    }
}
