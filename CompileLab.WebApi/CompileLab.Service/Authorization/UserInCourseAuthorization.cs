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
    public class UserInCourseAuthorization(IRepository<UserInCourse> repository) : IAuthorization
    {
        private readonly IRepository<UserInCourse> _repository = repository;
        public async Task<bool> IsOwnerOf(int targetId, int userId)
        {
            var result = await _repository.GetById(targetId);
            if (result == null)
                return false;
            
            return result.UserId == userId;
        }
    }
}
