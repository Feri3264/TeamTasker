namespace Tasker.Contracts.User.Register;

public record RegisterRequestDto
    (string name,
        string email,
        string password);