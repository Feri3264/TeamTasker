namespace Tasker.Contracts.Task.Create;

public record CreateTaskResponseDto(
    Guid taskId,
    string name,
    string status,
    string priority,
    Guid memberId,
    DateTime deadline,
    Guid projectId);