namespace Tasker.Contracts.User.SearchByEmail;

public record SearchByEmailResponseDto(
    Guid userId,
    string name,
    string email);