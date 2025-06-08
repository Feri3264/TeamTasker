namespace Tasker.Contracts.Session.Create;

public record CreateSessionResponseDto(
    Guid Id,
    string name,
    Guid ownerId);