namespace Tasker.Contracts.TeamMember.AddTeamMember;

public record AddTeamMemberResponseDto(
    Guid Id,
    Guid teamId,
    Guid userId);