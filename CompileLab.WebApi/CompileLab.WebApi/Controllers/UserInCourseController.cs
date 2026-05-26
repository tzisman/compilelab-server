using CompileLab.Repository.Entities;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using CompileLab.Service.Services;
using CompileLab.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompileLab.WebApi.Controllers
{
    [Authorize]
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

        [HttpGet("GetReport/{courseId}")]
        public async Task<IActionResult> GetReport(int courseId)
        {
            var report = await _service.GetCourseReportAsync(courseId);
            
            return Ok(report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] StatusDto status, int id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            var result = await _service.UpdateItem(id, status.Status, userId.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
