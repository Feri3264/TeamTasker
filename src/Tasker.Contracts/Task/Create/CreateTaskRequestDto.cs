namespace Tasker.Contracts.Task.Create;

public record CreateTaskRequestDto(
    string name,
    string status,
    string priority,
    Guid assignedMemberId,
    Guid projectId,
    DateTime deadline);