using CheckBox.Service.Dto;
using CheckBox.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CheckBox.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentAnswerController(IService<StudentAnswerDto> service) : ControllerBase
    {
        private readonly IService<StudentAnswerDto> _service = service;

        [HttpPost]
    public async Task<IActionResult> AddItem([FromBody] StudentAnswerDto studentAnswerDto)
    {
        try
        {
            var result = await _service.AddItem(studentAnswerDto);
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
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var studentAnswers = await _service.GetAll();
            return Ok(studentAnswers);
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
            var studentAnswer = await _service.GetById(id);
            if (studentAnswer == null)
            {
                return NotFound();
            }
            return Ok(studentAnswer);
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
    public async Task<IActionResult> Update([FromBody] StudentAnswerDto studentAnswer, int id)
    {
        try
        {
            var newStudentAnswer = await _service.UpdateItem(id, studentAnswer);
            if (newStudentAnswer == null)
            {
                return NotFound();
            }
            return Ok(newStudentAnswer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    }

}

