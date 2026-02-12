using CompileLab.Repository.Interfaces;
using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using CompileLab.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace CompileLab.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IRegister<UserRegisterDto> registerService, ILogin<UserLoginDto> loginSevice, IUserService service) : ControllerBase
    {
        private readonly IRegister<UserRegisterDto> _registerService = registerService;
        private readonly ILogin<UserLoginDto> _loginService = loginSevice;
        private readonly IUserService _service = service;
        


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            var result = await _registerService.Register(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            var result = await _loginService.Login(user);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _service.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteItem(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserDto user, int id)
        {
            var newUser = await _service.UpdateItem(id, user);
            if (newUser == null)
            {
                return NotFound();
            }
            return Ok(newUser);
        }
    }
}
