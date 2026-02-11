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
    public class CourseService(IRepository<Course> repository, IMapper mapper,
        [FromKeyedServices("course")] IAuthorization courseAuth
        ) : IService<CourseDto>
    {
        private readonly IRepository<Course> _repository = repository;
        private readonly IMapper _mapper = mapper;
        IAuthorization _courseAuth = courseAuth;
        public async Task<CourseDto> AddItem(CourseDto item)
        {
            var course = _mapper.Map<Course>(item);
            var result = await _repository.AddItem(course);
            var newCourse = _mapper.Map<CourseDto>(result);
            return newCourse;
        }

        public async Task<CourseDto> AddItem(CourseDto item, int userId)
        {
            if(item.LecturerId != userId)
            {
                throw new ForbiddenAccessException("User is not authorized to add a course.");
            }
            var course = _mapper.Map<Course>(item);
            var result = await _repository.AddItem(course);
            var newCourse = _mapper.Map<CourseDto>(result);
            return newCourse;
        }

        public async Task DeleteItem(int id)
        {
            await _repository.DeleteItem(id);
        }

        public async Task DeleteItem(int id, int userId)
        {
            if (!await _courseAuth.IsOwnerOf(id, userId))
            {
                throw new ForbiddenAccessException("User is not authorized to delete a course.");
            }
            var item = await _repository.GetById(id);
            if (item == null)
            {
                throw new KeyNotFoundException("Course not found.");
            }
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

        public async Task<CourseDto> UpdateItem(int id, CourseDto item, int userId)
        {
            if (!await _courseAuth.IsOwnerOf(id, userId) || item.LecturerId != userId)
            {
                throw new ForbiddenAccessException("User is not authorized to delete a course.");
            }

            var course = _mapper.Map<Course>(item);
            var result = await _repository.UpdateItem(id, course);
            var courseDto = _mapper.Map<CourseDto>(result);
            return courseDto;
        }
    }
}
