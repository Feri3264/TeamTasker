using ErrorOr;
using MediatR;
using Tasker.Shared.Enums;

namespace Tasker.Application.Tasks.Command.ChangeStatus;

public record TaskChangeStatusCommand
    (Guid id , TaskStatusEnum status) : IRequest<ErrorOr<Success>>;