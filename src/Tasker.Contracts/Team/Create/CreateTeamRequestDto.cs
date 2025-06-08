namespace Tasker.Contracts.Team.Create;

public record CreateTeamRequestDto(
    string name,
    Guid leadId);