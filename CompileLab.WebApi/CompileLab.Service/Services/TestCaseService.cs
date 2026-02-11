using AutoMapper;
using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Services
{
    public class TestCaseService(IRepository<TestCase> repository, IMapper mapper,
        [FromKeyedServices("testCase")] IAuthorization testCaseAuth,
        [FromKeyedServices("exercise")] IAuthorization exerciseAuth
        ) : IService<TestCaseDto>
    {
        private readonly IRepository<TestCase> _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorization _testCaseAuth = testCaseAuth;
        private readonly IAuthorization _exerciseAuth = exerciseAuth;

        public async Task<TestCaseDto> AddItem(TestCaseDto item)
        {
            var testCase = _mapper.Map<TestCase>(item);
            var result = await _repository.AddItem(testCase);
            var newTestCase = _mapper.Map<TestCaseDto>(result);
            return newTestCase;
        }

        public async Task<TestCaseDto> AddItem(TestCaseDto item, int userId)
        {

            if (!await _exerciseAuth.IsOwnerOf(item.ExerciseId, userId))
            {
                throw new ForbiddenAccessException("User is not authorized to delete a course.");
            }
            var testCase = _mapper.Map<TestCase>(item);
            var result = await _repository.AddItem(testCase);
            var newTestCase = _mapper.Map<TestCaseDto>(result);
            return newTestCase;
        }

        public async Task DeleteItem(int id)
        {
            await _repository.DeleteItem(id);
        }

        public async Task DeleteItem(int id, int userId)
        {
            if (!await _testCaseAuth.IsOwnerOf(id, userId))
            {
                throw new ForbiddenAccessException("User is not authorized to delete a course.");
            }
            await _repository.DeleteItem(id);
        }

        public async Task<List<TestCaseDto>> GetAll()
        {
            var result = await _repository.GetAll();
            var resultDto = _mapper.Map<List<TestCaseDto>>(result);
            return resultDto;
        }

        public async Task<TestCaseDto> GetById(int id)
        {
            var result = await _repository.GetById(id);
            var resultDto = _mapper.Map<TestCaseDto>(result);
            return resultDto;
        }

        public async Task<TestCaseDto> UpdateItem(int id, TestCaseDto item)
        {
            var testCase = _mapper.Map<TestCase>(item);
            var result = await _repository.UpdateItem(id, testCase);
            var resultDto = _mapper.Map<TestCaseDto>(result);
            return resultDto;
        }

        public async Task<TestCaseDto> UpdateItem(int id, TestCaseDto item, int userId)
        {
            if (!await _testCaseAuth.IsOwnerOf(id, userId) || !await _exerciseAuth.IsOwnerOf(item.ExerciseId,userId))
            {
                throw new ForbiddenAccessException("User is not authorized to delete a course.");
            }
            var testCase = _mapper.Map<TestCase>(item);
            var result = await _repository.UpdateItem(id, testCase);
            var resultDto = _mapper.Map<TestCaseDto>(result);
            return resultDto;
        }
    }
}
