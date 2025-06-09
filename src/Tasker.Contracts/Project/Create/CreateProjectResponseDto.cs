namespace Tasker.Contracts.Project.Create;

public record CreateProjectResponseDto(
    Guid projectId,
    string name,
    Guid leadId);