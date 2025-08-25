using LearnTrack.Application.Auth.Interfaces;
using LearnTrack.Application.Exceptions;
using LearnTrack.Application.Users;
using LearnTrack.Domain.Entities;
using LearnTrack.Domain.Enums;

namespace LearnTrack.Application.Auth;

public class AuthService(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IUserRepository userRepository
) : IAuthService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var existingByEmail = await _userRepository.GetByEmailAsync(request.Email);
        if (existingByEmail is not null)
        {
            throw new ConflictException("Email already in use");
        }

        var existingByUsername = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingByUsername is not null)
        {
            throw new ConflictException("Username already in use");
        }

        var (hash, salt) = _passwordHasher.Hash(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hash,
            PasswordSalt = salt,
            Role = UserRole.User,
        };

        await _userRepository.CreateAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthResult(token, user.Username, user.Role.ToString());
    }

    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new Exception("Invalid credentials");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthResult(token, user.Username, user.Role.ToString());
    }
}