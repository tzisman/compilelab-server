using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CompileLab.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestCaseController(IService<TestCaseDto> service) : ControllerBase
    {
        private readonly IService<TestCaseDto> _service = service;
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] TestCaseDto testCaseDto)
        {
            try
            {
                var result = await _service.AddItem(testCaseDto);
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
                var TestCasesDto = await _service.GetAll();
                return Ok(TestCasesDto);
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
                var result = await _service.GetById(id);
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
        public async Task<IActionResult> Update([FromBody] TestCaseDto testCaseDto, int id)
        {
            try
            {
                var result = await _service.UpdateItem(id, testCaseDto);
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
