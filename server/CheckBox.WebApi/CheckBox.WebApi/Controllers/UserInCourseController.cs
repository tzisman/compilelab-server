using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheckBox.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInCourseController(IService<UserInCourseDto> service) : ControllerBase
    {
        private readonly IService<UserInCourseDto> _service = service;

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] UserInCourseDto userInCourseDto)
        {
            try
            {
                var result = await _service.AddItem(userInCourseDto);
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
                var useresInCourses = await _service.GetAll();
                return Ok(useresInCourses);
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
                var userInCourse = await _service.GetById(id);
                if (userInCourse == null)
                {
                    return NotFound();
                }
                return Ok(userInCourse);
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
        public async Task<IActionResult> Update([FromBody] UserInCourseDto userInCourseDto, int id)
        {
            try
            {
                var result= await _service.UpdateItem(id, userInCourseDto);
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
    }
}
