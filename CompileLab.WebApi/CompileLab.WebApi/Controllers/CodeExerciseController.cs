using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CompileLab.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeExerciseController(IService<CodeExerciseDto> service) : ControllerBase
    {
        private readonly IService<CodeExerciseDto> _service = service;

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] CodeExerciseDto codeExerciseDto)
        {
            try
            {
                var result = await _service.AddItem(codeExerciseDto);
                if (result == null)
                {
                    return BadRequest("Could not create the item.");
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
                var codeExercisesDto = await _service.GetAll();
                return Ok(codeExercisesDto);
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
        public async Task<IActionResult> Update([FromBody] CodeExerciseDto codeExerciseDto, int id)
        {
            try
            {
                var result = await _service.UpdateItem(id, codeExerciseDto);
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
