using CompileLab.Repository.Entities;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using CompileLab.Service.Services;
using CompileLab.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CompileLab.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInCourseController(IUserInCourseService service) : ControllerBase
    {
        private readonly IUserInCourseService _service = service;

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] UserInCourseDto userInCourseDto)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            var result = await _service.AddItem(userInCourseDto, userId.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            var useresInCourses = await _service.GetAll();
            return Ok(useresInCourses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
             var userInCourse = await _service.GetById(id);
             if (userInCourse == null)
             {
                 return NotFound();
             }
             return Ok(userInCourse);
        }

        [HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var userId = User.GetUserId();
        //    if (userId == null)
        //    {
        //        return Unauthorized("You are not logged in.");
        //    }
        //    await _service.DeleteItem(id, userId.Value);
        //    return NoContent();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CourseStatus status, int id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            var result = await _service.UpdateItem(id, status, userId.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
