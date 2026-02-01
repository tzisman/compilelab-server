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
    public class CourseService(IRepository<Course> repository, IMapper mapper) : IService<CourseDto>
    {
        private readonly IRepository<Course> _repository = repository;
        private readonly IMapper _mapper = mapper;
        public async Task<CourseDto> AddItem(CourseDto item)
        {
            var course = _mapper.Map<Course>(item);
            var result = await _repository.AddItem(course);
            var newCourse = _mapper.Map<CourseDto>(result);
            return newCourse;
        }

        public async Task DeleteItem(int id)
        {
            await _repository.DeleteItem(id);
        }

        public async Task<List<CourseDto>> GetAll()
        {
            var courses = await _repository.GetAll();
            var coursesDto = _mapper.Map<List<CourseDto>>(courses);
            return coursesDto;
        }

        public async Task<CourseDto> GetById(int id)
        {
            var course = await _repository.GetById(id);
            var courseDto = _mapper.Map<CourseDto>(course);
            return courseDto;
        }

        public async Task<CourseDto> UpdateItem(int id, CourseDto item)
        {
            var course = _mapper.Map<Course>(item);
            var result = await _repository.UpdateItem(id, course);
            var courseDto = _mapper.Map<CourseDto>(result);
            return courseDto;
        }
    }
}
