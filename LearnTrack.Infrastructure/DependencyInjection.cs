using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LearnTrack.Application.Auth;
using LearnTrack.Application.Auth.Interfaces;
using LearnTrack.Application.Topics;
using LearnTrack.Application.Topics.Interfaces;
using LearnTrack.Application.Users;
using LearnTrack.Infrastructure.Auth;
using LearnTrack.Infrastructure.Persistence;
using LearnTrack.Infrastructure.Repositories;

namespace LearnTrack.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
    (
        this IServiceCollection services, 
        IConfiguration configuration
    )
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>(); 
        services.AddScoped<ITopicRepository, TopicRepository>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITopicService, TopicService>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
