using LearnTrack.Application.Auth.Interfaces;
using LearnTrack.Application.Exceptions;
using LearnTrack.Application.Topics.Interfaces;
using LearnTrack.Domain.Entities;

namespace LearnTrack.Application.Topics;

public class TopicService(
    ITopicRepository topicRepository,
    ICurrentUserService currentUserService
) : ITopicService
{
    private readonly ITopicRepository _topicRepository = topicRepository;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<TopicDto> CreateAsync(TopicRequest request)
    {
        var userId = _currentUserService.GetUserId();

        var topic = new Topic
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Tags = request.Tags,
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
            IsDone = false,
        };

        await _topicRepository.CreateAsync(topic);

        return ConvertToDto(topic);
    }

    public async Task<IEnumerable<TopicDto>> GetAllAsync()
    {
        var userId = _currentUserService.GetUserId();
        var topics = await _topicRepository.GetAllByUserIdAsync(userId);
        return topics.Select(ConvertToDto);
    }

    public async Task<TopicDto?> GetByIdAsync(Guid id)
    {
        var userId = _currentUserService.GetUserId();
        var topic = await _topicRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Topic with ID '{id}' not found.");

        if (topic.UserId != userId)
            throw new ForbiddenException("You do not have access to this topic.");

        return ConvertToDto(topic);
    }

    public async Task UpdateAsync(Guid id, TopicRequest request)
    {
        var userId = _currentUserService.GetUserId();
        var topic = await _topicRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Topic with ID '{id}' not found.");

        if (topic.UserId != userId)
            throw new ForbiddenException("You cannot update this topic.");

        topic.Name = request.Name;
        topic.Description = request.Description;
        topic.Tags = request.Tags;

        await _topicRepository.UpdateAsync(topic);
    }

    public async Task SetDoneStatusAsync(Guid id, TopicStatusRequest request)
    {
        var userId = _currentUserService.GetUserId();
        var topic = await _topicRepository.GetByIdAsync(id)
                    ?? throw new NotFoundException($"Topic with ID '{id}' not found.");

        if (topic.UserId != userId)
            throw new ForbiddenException("You cannot update this topic.");

        topic.IsDone = request.IsDone;

        await _topicRepository.UpdateAsync(topic);
    }

    public async Task DeleteAsync(Guid id)
    {
        var userId = _currentUserService.GetUserId();
        var topic = await _topicRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Topic with ID '{id}' not found.");

        if (topic.UserId != userId)
            throw new ForbiddenException("You cannot delete this topic.");

        await _topicRepository.DeleteAsync(topic);
    }

    private static TopicDto ConvertToDto(Topic topic)
    {
        return new TopicDto
        {
            Id = topic.Id,
            Name = topic.Name,
            Description = topic.Description,
            Tags = topic.Tags,
            CreatedAt = topic.CreatedAt
        };
    }
}
