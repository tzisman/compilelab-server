using CheckBox.Repository.Entities;
using CheckBox.Repository.Interfaces;
using CheckBox.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Authorization
{
    public class TestCaseAuthorization(IRepository<TestCase> repository) : IAuthorization
    {
        private readonly IRepository<TestCase> _repository = repository;
        public async Task<bool> IsOwnerOf(int targetId, int userId)
        {
            var result = await _repository.GetById(targetId);
            if (result == null)
                return false;
            return result.Exercise.Course.LecturerId == userId;
        }
    }
}