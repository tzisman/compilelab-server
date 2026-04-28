using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using CompileLab.Service.Services;
using CompileLab.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace CompileLab.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            var token = await _service.Register(user);
            var newUser = await _service.GetUserByEmail(user.Email);
            return Ok(new { token = token, user = newUser });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            var token = await _service.Login(user);
            var newUser = await _service.GetUserByEmail(user.Email);
            return Ok(new {token = token, user = newUser});
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = User.GetUserId();

            var isAdmin = User.IsInRole("Admin");

            if (userId == null || (userId != id && !isAdmin))
            {
                return Forbid();
            }

            var user = await _service.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet("courses")]
        public async Task<IActionResult> GetCoursesByUser()
        {
            var userId = User.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var courses = await _service.GetCourseOfUser(userId.Value);
            return Ok(courses);
        }

        [HttpGet("lecturers")]
        public async Task<IActionResult> GetCoursesByLecturer()
        {
            var userId = User.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var courses = await _service.GetCourseOfLecturer(userId.Value);
            return Ok(courses);
        }

        [HttpGet("requests")]
        public async Task<IActionResult> GetReqwestByUser()
        {
            var userId = User.GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var courses = await _service.GetReqwestOfUser(userId.Value);
            return Ok(courses);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetUserId();

            var isAdmin = User.IsInRole("Admin");

            if (userId == null || (userId != id && !isAdmin))
            {
                return Forbid();
            }

            await _service.DeleteItem(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserDto user, int id)
        {
            var userId = User.GetUserId();

            var isAdmin = User.IsInRole("Admin");

            if (userId == null || (userId != id && !isAdmin))
            {
                return Forbid();
            }

            var newUser = await _service.UpdateItem(id, user);
            if (newUser == null)
            {
                return NotFound();
            }
            return Ok(newUser);
        }
    }
}
