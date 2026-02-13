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
    public class UserInCourseService(IUserInCourseRepository repository, IMapper mapper,
        IUserInCourseAuthorization uicAuth
        ) : IUserInCourseService
    {
        private readonly IUserInCourseRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUserInCourseAuthorization _uicAuth = uicAuth;


        public async Task<UserInCourseDto> AddItem(UserInCourseDto item, int userId)
        {
            if (item.UserId != userId)
            {
                throw new ForbiddenAccessException("User is not authorized to add a course.");
            }

            var uic = await _repository.GetByUserAndCourse(item.CourseId, item.UserId);
            if (uic != null)
            {
                throw new InvalidOperationException("You have already signed up for this course.");
            }

            var userInCourse = _mapper.Map<UserInCourse>(item);
            var result = await _repository.AddItem(userInCourse);
            var newCourse = _mapper.Map<UserInCourseDto>(result);
            return newCourse;
        }


        public async Task DeleteItem(int id, int userId)
        {
            if (!await _uicAuth.IsOwnerOf(id, userId))
            {
                throw new ForbiddenAccessException("User is not authorized to delete a userInCourse.");
            }
            await _repository.DeleteItem(id);
        }

        public async Task<List<UserInCourseDto>> GetAll()
        {
            var result = await _repository.GetAll();
            var resultDto = _mapper.Map<List<UserInCourseDto>>(result);
            return resultDto;
        }

        public async Task<UserInCourseDto> GetById(int id)
        {
            var result = await _repository.GetById(id);
            var resultDto = _mapper.Map<UserInCourseDto>(result);
            return resultDto;
        }


        public async Task<UserInCourseDto> UpdateItem(int id, CourseStatus status, int userId)
        {
            if (!await _uicAuth.IsLectureOf(id, userId))
            {
                throw new ForbiddenAccessException("User is not authorized to update a course.");
            }
            var userInCourse = await _repository.GetById(id);
            userInCourse.Status = status;
            var result = await _repository.UpdateItem(id, userInCourse);
            var resultDto = _mapper.Map<UserInCourseDto>(result);
            return resultDto;
        }
    }
}
