namespace Tasker.Contracts.Team.GetSessionTeams;

public record GetSessionTeamsResponseDto(
    Guid teamId,
    string name,
    Guid teamLeadId);