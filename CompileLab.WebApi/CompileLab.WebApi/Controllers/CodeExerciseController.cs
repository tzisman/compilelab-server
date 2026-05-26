using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using CompileLab.Service.Services;
using CompileLab.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompileLab.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CodeExerciseController(IExerciseService service) : ControllerBase
    {
        private readonly IExerciseService _service = service;

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] CodeExerciseDto codeExerciseDto)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            var result = await _service.AddItem(codeExerciseDto, userId.Value);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            var codeExercisesDto = await _service.GetAll();
            return Ok(codeExercisesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CodeExerciseDto codeExerciseDto, int id)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            var result = await _service.UpdateItem(id, codeExerciseDto, userId.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourseId(int courseId)
        {
            var result = await _service.GetExercisesByCourseId(courseId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("studentExercises/{courseId}")]
        public async Task<IActionResult> GetMyExercises(int courseId)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }
            var result = await _service.GetStudentExerciseListAsync(courseId, userId.Value);
            return Ok(result);
        }


        [HttpGet("{exerciseId}/student/")]
        public async Task<IActionResult> GetExerciseForStudent(int exerciseId)
        {
            var userId = User.GetUserId();
            if (userId == null)
            {
                return Unauthorized("You are not logged in.");
            }

            var result = await _service.GetExerciseForStudent(exerciseId, userId.Value);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
