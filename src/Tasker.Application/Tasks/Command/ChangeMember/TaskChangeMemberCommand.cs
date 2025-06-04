using ErrorOr;
using MediatR;

namespace Tasker.Application.Tasks.Command.ChangeMember;

public record TaskChangeMemberCommand
    (Guid id , Guid userId) : IRequest<ErrorOr<Success>>;