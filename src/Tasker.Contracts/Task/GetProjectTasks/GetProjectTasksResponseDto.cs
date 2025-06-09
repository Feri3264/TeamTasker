namespace Tasker.Contracts.Task.GetProjectTasks;

public record GetProjectTasksResponseDto(
    Guid taskId,
    string name,
    string status,
    string priority,
    Guid memberId,
    DateTime deadline,
    Guid projectId);