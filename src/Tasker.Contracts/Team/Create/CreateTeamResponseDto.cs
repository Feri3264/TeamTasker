namespace Tasker.Contracts.Team.Create;

public record CreateTeamResponseDto(
    Guid teamId,
    string name,
    Guid leadId);