namespace LearnTrack.Application.Topics.Interfaces;

public interface ITopicService
{
    Task<TopicDto> CreateAsync(TopicRequest request);
    Task<IEnumerable<TopicDto>> GetAllAsync();
    Task<TopicDto?> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(Guid id, TopicRequest request);
    Task SetDoneStatusAsync(Guid id, TopicStatusRequest request);
}
