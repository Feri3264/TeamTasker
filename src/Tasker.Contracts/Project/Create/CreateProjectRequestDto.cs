namespace Tasker.Contracts.Project.Create;

public record CreateProjectRequestDto(
    string name,
    Guid leadId);