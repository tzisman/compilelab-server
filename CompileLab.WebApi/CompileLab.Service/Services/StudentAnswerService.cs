using AutoMapper;
using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Services
{
    public class StudentAnswerService(IRepository<StudentAnswer> repository, IMapper mapper,
        IAnswerAuthorization answerAuth,
        IUserInCourseAuthorization uicAuth
        ) : IService<StudentAnswerDto>
    {
        private readonly IRepository<StudentAnswer> _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUserInCourseAuthorization _uicAuth = uicAuth;
        private readonly IAnswerAuthorization _answerAuth = answerAuth;
        public async Task<StudentAnswerDto> AddItem(StudentAnswerDto item)
        {
            var studentAnswer = _mapper.Map<StudentAnswer>(item);
            var result = await _repository.AddItem(studentAnswer);
            var newStudentAnswer = _mapper.Map<StudentAnswerDto>(result);
            return newStudentAnswer;
        }

        public async Task<StudentAnswerDto> AddItem(StudentAnswerDto item, int userId)
        {
            if (!await _uicAuth.IsOwnerOf(item.UserInCourseId, userId)
                || !await _uicAuth.IsAllowedToChange(item.UserInCourseId)
                || !await _uicAuth.IsInCourse(item.ExerciseId, item.UserInCourseId))
            {
                throw new ForbiddenAccessException("User is not authorized to delete a course.");
            }
            var studentAnswer = _mapper.Map<StudentAnswer>(item);
            var result = await _repository.AddItem(studentAnswer);
            var newStudentAnswer = _mapper.Map<StudentAnswerDto>(result);
            return newStudentAnswer;
        }

        public async Task DeleteItem(int id)
        {
            await _repository.DeleteItem(id);
        }

        public async Task DeleteItem(int id, int userId)
        {
            if (!await _answerAuth.IsOwnerOf(id, userId)
                || !await _answerAuth.IsAllowedToChange(id))
            {
                throw new ForbiddenAccessException("User is not authorized to delete a course.");
            }
            await _repository.DeleteItem(id);
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

        public async Task<StudentAnswerDto> UpdateItem(int id, StudentAnswerDto item, int userId)
        {
            var existingAnswer = await _repository.GetById(id);
            if (existingAnswer == null)
                throw new KeyNotFoundException();
            if (!await _uicAuth.IsOwnerOf(existingAnswer.UserInCourseId, userId)
                || !await _uicAuth.IsAllowedToChange(existingAnswer.UserInCourseId)
                || !await _uicAuth.IsInCourse(existingAnswer.ExerciseId, existingAnswer.UserInCourseId))
            {
                throw new ForbiddenAccessException("User is not authorized to update this answer.");
            }
            var studentAnswer = _mapper.Map<StudentAnswer>(item);
            var result = await _repository.UpdateItem(id, studentAnswer);
            var resultDto = _mapper.Map<StudentAnswerDto>(result);
            return resultDto;
        }
    }
}
