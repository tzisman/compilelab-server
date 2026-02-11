using CompileLab.Service.Dto;
using CompileLab.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CompileLab.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentAnswerController(IService<StudentAnswerDto> service) : ControllerBase
    {
        private readonly IService<StudentAnswerDto> _service = service;

        [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] StudentAnswerDto studentAnswerDto)
    {
        var result = await _service.AddItem(studentAnswerDto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {

        var studentAnswers = await _service.GetAll();
        return Ok(studentAnswers);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {

        var studentAnswer = await _service.GetById(id);
        if (studentAnswer == null)
        {
            return NotFound();
        }
        return Ok(studentAnswer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteItem(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] StudentAnswerDto studentAnswer, int id)
    {

            var newStudentAnswer = await _service.UpdateItem(id, studentAnswer);
            if (newStudentAnswer == null)
            {
                return NotFound();
            }
            return Ok(newStudentAnswer);
       
    }
    }

}

