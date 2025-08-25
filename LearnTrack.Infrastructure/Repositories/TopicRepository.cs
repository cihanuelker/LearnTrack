using Microsoft.EntityFrameworkCore;
using LearnTrack.Application.Topics.Interfaces;
using LearnTrack.Domain.Entities;
using LearnTrack.Infrastructure.Persistence;

namespace LearnTrack.Infrastructure.Repositories;

public class TopicRepository(AppDbContext db) : ITopicRepository
{
    private readonly AppDbContext _db = db;

    public async Task<List<Topic>> GetAllByUserIdAsync(Guid userId) =>
        await _db.Topics.Where(t => t.UserId == userId).ToListAsync();

    public async Task<Topic?> GetByIdAsync(Guid id) =>
        await _db.Topics.FirstOrDefaultAsync(t => t.Id == id);

    public async Task CreateAsync(Topic topic)
    {
        await _db.Topics.AddAsync(topic);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Topic topic)
    {
        _db.Topics.Update(topic);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Topic topic)
    {
        _db.Topics.Remove(topic);
        await _db.SaveChangesAsync();
    }
}
