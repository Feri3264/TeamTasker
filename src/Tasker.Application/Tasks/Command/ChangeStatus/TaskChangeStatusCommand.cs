using ErrorOr;
using MediatR;

namespace Tasker.Application.Tasks.Command.ChangeStatus;

public record TaskChangeStatusCommand
    (Guid id , string status) : IRequest<ErrorOr<Success>>;