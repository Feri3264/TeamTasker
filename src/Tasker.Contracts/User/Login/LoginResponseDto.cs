namespace Tasker.Contracts.User.Login;

public record LoginResponseDto
    (Guid userId,
        string name,
        string email,
        string password,
        string refreshToken,
        string token);