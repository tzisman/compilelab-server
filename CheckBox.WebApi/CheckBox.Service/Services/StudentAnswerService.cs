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
    public class StudentAnswerService(IRepository<StudentAnswer> repository, IMapper mapper) : IService<StudentAnswerDto>
    {
        private readonly IRepository<StudentAnswer> _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<StudentAnswerDto> AddItem(StudentAnswerDto item)
        {
            var studentAnswer = _mapper.Map<StudentAnswer>(item);
            var result = await _repository.AddItem(studentAnswer);
            var newStudentAnswer = _mapper.Map<StudentAnswerDto>(result);
            return newStudentAnswer;
        }

        public Task<StudentAnswerDto> AddItem(StudentAnswerDto item, int userId)
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

        public async Task<List<StudentAnswerDto>> GetAll()
        {
            var result = await _repository.GetAll();
            var resultDto = _mapper.Map<List<StudentAnswerDto>>(result);
            return resultDto;
        }

        public async Task<StudentAnswerDto> GetById(int id)
        {
            var result = await _repository.GetById(id);
            var resultDto = _mapper.Map<StudentAnswerDto>(result);
            return resultDto;
        }

        public async Task<StudentAnswerDto> UpdateItem(int id, StudentAnswerDto item)
        {
            var studentAnswer = _mapper.Map<StudentAnswer>(item);
            var result = await _repository.UpdateItem(id, studentAnswer);
            var resultDto = _mapper.Map<StudentAnswerDto>(result);
            return resultDto;
        }

        public Task<StudentAnswerDto> UpdateItem(int id, StudentAnswerDto item, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
