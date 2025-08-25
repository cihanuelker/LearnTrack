using Microsoft.AspNetCore.Http;
using LearnTrack.Application.Auth.Interfaces;
using System.Security.Claims;

namespace LearnTrack.Infrastructure.Auth;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User not authenticated");

        return Guid.Parse(userIdClaim.Value);
    }
}
