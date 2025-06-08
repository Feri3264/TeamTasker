namespace Tasker.Contracts.SessionMember.GetSessionMembers;

public record GetSessionMembersResponseDto(
    Guid userId,
    string name,
    string email);