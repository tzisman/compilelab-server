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
    public class UserInCourseService(IRepository<UserInCourse> repository, IMapper mapper) : IService<UserInCourseDto>
    {
        private readonly IRepository<UserInCourse> _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<UserInCourseDto> AddItem(UserInCourseDto item)
        {
            var userInCourse = _mapper.Map<UserInCourse>(item);
            var result = await _repository.AddItem(userInCourse);
            var newCourse = _mapper.Map<UserInCourseDto>(result);
            return newCourse;
        }

        public async Task DeleteItem(int id)
        {
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

        public async Task<UserInCourseDto> UpdateItem(int id, UserInCourseDto item)
        {
            var userInCourse = _mapper.Map<UserInCourse>(item);
            var result = await _repository.UpdateItem(id, userInCourse);
            var resultDto = _mapper.Map<UserInCourseDto>(result);
            return resultDto;
        }
    }
}
