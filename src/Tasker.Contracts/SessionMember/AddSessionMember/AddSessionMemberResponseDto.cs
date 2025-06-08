namespace Tasker.Contracts.SessionMember.AddSessionMember;

public record AddSessionMemberResponseDto(
    Guid Id,
    Guid sessionId,
    Guid userId);