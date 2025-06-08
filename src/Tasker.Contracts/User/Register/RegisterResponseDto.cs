namespace Tasker.Contracts.User.Register;

public record RegisterResponseDto
    (Guid userId,
        string name,
        string email,
        string password,
        bool isDelete);