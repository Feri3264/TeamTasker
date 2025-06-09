using ErrorOr;
using MediatR;
using Tasker.Domain.Tasks;
using Tasker.Shared.Enums;

namespace Tasker.Application.Tasks.Command.Create;

public record CreateTaskCommand
    (string name,
        string status,
        string priority,
        Guid assignedMemberId,
        Guid projectId,
        DateTime deadline) : IRequest<ErrorOr<TaskModel>>;