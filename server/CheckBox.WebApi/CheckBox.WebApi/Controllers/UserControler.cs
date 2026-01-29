using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheckBox.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRegister<UserRegisterDto> _service;

        public UserController(IRegister<UserRegisterDto> service)
        {
            _service = service;
        }
            
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            try
            {
                var result = await _service.Register(user);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<List<BabyDto>> GetALl()
        {
            return await service.GetAll();
        }
    }
}
