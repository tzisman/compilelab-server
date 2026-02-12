using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using CompileLab.Service.Services;
using CompileLab.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompileLab.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController(IService<CourseDto> service) : ControllerBase
    {
        private readonly IService<CourseDto> _service = service;
        
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] CourseDto courseDto)
        {
           var userId = User.GetUserId();
           if(userId == null)
           {
               return Unauthorized("You are not logged in.");
           }
           var result = await _service.AddItem(courseDto, userId.Value);
           return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            var courses = await _service.GetAll();
            return Ok(courses);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _service.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            await _service.DeleteItem(id, userId.Value);
            return NoContent();  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]  CourseDto courseDto, int id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
              var newCourse = await _service.UpdateItem(id, courseDto, userId.Value);
              if (newCourse == null)
              {
                  return NotFound();
              }
              return Ok(newCourse);   
        }
        
    }
}
