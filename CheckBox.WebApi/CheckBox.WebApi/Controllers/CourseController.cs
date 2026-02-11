using CheckBox.Repository.Entities;
using CheckBox.Repository.Interfaces;
using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using CheckBox.Service.Services;
using CheckBox.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheckBox.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController(IService<CourseDto> service) : ControllerBase
    {
        private readonly IService<CourseDto> _service = service;
        
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] CourseDto courseDto)
        {
            try
            {
                var userId = User.GetUserId();
                if(userId == null)
                {
                    return Unauthorized("You are not logged in.");
                }
                var result = await _service.AddItem(courseDto, userId.Value);
                return Ok(result);
            }
            catch (ForbiddenAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            try
            {
                var courses = await _service.GetAll();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var course = await _service.GetById(id);
                if (course == null)
                {
                    return NotFound();
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            try
            {
                await _service.DeleteItem(id, userId.Value);
                return NoContent();  
            }
            catch (ForbiddenAccessException)
            {
                return Forbid();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]  CourseDto courseDto, int id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            try
            {
                var newCourse = await _service.UpdateItem(id, courseDto, userId.Value);
                if (newCourse == null)
                {
                    return NotFound();
                }
                return Ok(newCourse);   
            }
            catch (ForbiddenAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
