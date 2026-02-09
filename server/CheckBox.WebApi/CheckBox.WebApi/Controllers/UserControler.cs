using CheckBox.Repository.Interfaces;
using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using CheckBox.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace CheckBox.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IRegister<UserRegisterDto> registerService, ILogin<UserLoginDto> loginSevice, IService<UserDto> service) : ControllerBase
    {
        private readonly IRegister<UserRegisterDto> _registerService = registerService;
        private readonly ILogin<UserLoginDto> _loginService = loginSevice;
        private readonly IService<UserDto> _service = service;
        


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            try
            {
                var result = await _registerService.Register(user);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserLoginDto user)
        {
            try
            {
                var result = await _loginService.Login(user);
                if(result == "succes")
                {
                    return Ok(result);
                }
                return Unauthorized();
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
                var users =  await _service.GetAll();
                return Ok(users);
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
                var user = await _service.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
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
        public async Task<IActionResult> Update([FromBody] UserDto user, int id)
        {
            try
            {
                var newUser = await _service.UpdateItem(id, user);
                if (newUser == null)
                {
                    return NotFound();
                }
                return Ok(newUser);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
