using CheckBox.Repository.Entities;
using CheckBox.Repository.Interfaces;
using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheckBox.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController(IService<CourseDto> service) : Controller
    {
        private readonly IService<CourseDto> _service = service;
        
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] CourseDto courseDto)
        {
            try
            {
                var result = await _service.AddItem(courseDto);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
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
            try
            {
                await _service.DeleteItem(id);
                return NoContent();  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Post([FromForm]  CourseDto courseDto, int id)
        {
            try
            {
                var newCourse = await _service.UpdateItem(id, courseDto);
                if (newCourse == null)
                {
                    return NotFound();
                }
                return Ok(newCourse);   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
