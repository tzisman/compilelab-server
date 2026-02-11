using AutoMapper;
using CheckBox.Repository.Entities;
using CheckBox.Repository.Interfaces;
using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBox.Service.Services
{
    public class TestCaseService(IRepository<TestCase> repository, IMapper mapper) : IService<TestCaseDto>
    {
        private readonly IRepository<TestCase> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<TestCaseDto> AddItem(TestCaseDto item)
        {
            var testCase = _mapper.Map<TestCase>(item);
            var result = await _repository.AddItem(testCase);
            var newTestCase = _mapper.Map<TestCaseDto>(result);
            return newTestCase;
        }

        public Task<TestCaseDto> AddItem(TestCaseDto item, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteItem(int id)
        {
            await _repository.DeleteItem(id);
        }

        public Task DeleteItem(int id, int userId)
        {
            throw new NotImplementedException();
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

        public Task<TestCaseDto> UpdateItem(int id, TestCaseDto item, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
