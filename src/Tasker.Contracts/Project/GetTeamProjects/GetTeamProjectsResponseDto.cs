namespace Tasker.Contracts.Project.GetTeamProjects;

public record GetTeamProjectsResponseDto(
    Guid Id,
    string name,
    Guid leadId);