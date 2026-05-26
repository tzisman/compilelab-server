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
    public class TestCaseController(ITestCaseService service) : ControllerBase
    {
        private readonly ITestCaseService _service = service;
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] TestCaseDto testCaseDto)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            var result = await _service.AddItem(testCaseDto, userId.Value);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            await _service.DeleteItem(id, userId.Value);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] TestCaseDto testCaseDto, int id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            var result = await _service.UpdateItem(id, testCaseDto, userId.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("exercise/{exerciseId}")]
        public async Task<IActionResult> GetByExerciseId(int exerciseId)
        {
            var result = await _service.GetTestcaseByExerciseId(exerciseId);

            return Ok(result);
        }
    }
}
