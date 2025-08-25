using LearnTrack.Domain.Entities;

namespace LearnTrack.Application.Topics.Interfaces;

public interface ITopicRepository
{
    Task<List<Topic>> GetAllByUserIdAsync(Guid userId);
    Task<Topic?> GetByIdAsync(Guid id);
    Task CreateAsync(Topic topic);
    Task UpdateAsync(Topic topic);
    Task DeleteAsync(Topic topic);
}
