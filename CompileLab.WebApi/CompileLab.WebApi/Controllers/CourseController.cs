using CompileLab.Repository.Entities;
using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using CompileLab.Service.Services;
using CompileLab.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompileLab.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController(ICourseService service) : ControllerBase
    {
        private readonly ICourseService _service = service;
        
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
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string search = null)
        {
            var courses = await _service.GetAll(page, size, search);
            return Ok(courses);
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
