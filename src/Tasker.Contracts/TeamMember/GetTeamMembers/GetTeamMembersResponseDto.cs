namespace Tasker.Contracts.TeamMember.GetTeamMembers;

public record GetTeamMembersResponseDto(
    Guid userId,
    string name,
    string email);