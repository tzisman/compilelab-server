using AutoMapper;
using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CompileLab.Service.Services
{
    public class UserService(IUserRepository repository, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task DeleteItem(int id)
        {
            await _repository.DeleteItem(id);
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _repository.GetAll();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _repository.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<List<CourseDto>> GetCourseOfLetucrer(int id)
        {
            var courses = await _repository.GetCourseOfLecturer(id);
            var coursesDto = _mapper.Map<List<CourseDto>>(courses);
            return coursesDto;
        }

        public async Task<List<CourseDto>> GetCourseOfUser(int id)
        {
            var courses = await _repository.GetCourseOfUser(id);
            var coursesDto = _mapper.Map<List<CourseDto>>(courses);
            return coursesDto;
        }

        public async Task<List<CourseReqwestDto>> GetReqwestOfUser(int id)
        {
            var reqwests = await _repository.GetReqwestOfUser(id);
            var reqwestsDto = _mapper.Map<List<CourseReqwestDto>>(reqwests);

            return reqwestsDto;
        }

        public async Task<UserDto> UpdateItem(int id, UserDto item)
        {
            var email = await _repository.GetUserByEmail(item.Email);
            if (email != null && email.Id != id)
            {
                throw new InvalidOperationException("The email already exists.");
            }

            var user = _mapper.Map<User>(item);
            var result =await _repository.UpdateItem(id, user);
            var userDto = _mapper.Map<UserDto>(result);
            return userDto;
        }
    }
}
