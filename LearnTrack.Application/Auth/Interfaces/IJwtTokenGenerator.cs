using LearnTrack.Domain.Entities;

namespace LearnTrack.Application.Auth.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
