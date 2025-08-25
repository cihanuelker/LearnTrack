using Moq;
using LearnTrack.Application.Auth;
using LearnTrack.Application.Auth.Interfaces;
using LearnTrack.Application.Users;
using LearnTrack.Domain.Entities;

namespace LearnTrack.Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task RegisterAsync_ShouldReturnAuthResult_WhenUserIsValid()
    {
        // Arrange
        var jwtMock = new Mock<IJwtTokenGenerator>();
        var userRepoMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();

        var authService = new AuthService(jwtMock.Object, passwordHasherMock.Object, userRepoMock.Object);

        var request = new RegisterRequest("testuser", "password", "a@a");

        jwtMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("fake-jwt");

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert 
        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username);
        Assert.Equal("User", result.Role);
        Assert.Equal("fake-jwt", result.Token);

        userRepoMock.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Once);
    }
}