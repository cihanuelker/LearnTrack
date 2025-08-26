using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LearnTrack.Application.Topics;
using LearnTrack.Application.Topics.Interfaces;

namespace LearnTrack.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TopicsController(ITopicService topicService) : ControllerBase
{
    private readonly ITopicService _topicService = topicService;

    [HttpPost]
    public async Task<ActionResult<TopicDto>> Create(
    [FromServices] IValidator<TopicRequest> validator,
    [FromBody] TopicRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new ValidationException(
                string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage))
                );

        var topic = await _topicService.CreateAsync(request);
        return Ok(topic);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TopicDto>>> GetAll()
    {
        var topics = await _topicService.GetAllAsync();
        return Ok(topics);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TopicDto>> GetById(Guid id)
    {
        var topic = await _topicService.GetByIdAsync(id);

        return Ok(topic);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _topicService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TopicRequest request)
    {
        await _topicService.UpdateAsync(id, request);

        return NoContent();
    }
}
