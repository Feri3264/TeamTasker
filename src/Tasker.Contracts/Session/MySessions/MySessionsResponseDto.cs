namespace Tasker.Contracts.Session.MySessions;

public record MySessionsResponseDto(
    Guid sessionId,
    string name,
    Guid ownerId);