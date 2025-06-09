namespace Tasker.Contracts.Task.GetUserTasks;

public record GetUserTasksResponseDto(
    Guid taskId,
    string name,
    string status,
    string priority,
    Guid memberId,
    DateTime deadline,
    Guid projectId);