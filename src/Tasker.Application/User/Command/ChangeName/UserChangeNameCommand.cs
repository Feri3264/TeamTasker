using ErrorOr;
using MediatR;

namespace Tasker.Application.User.Command.ChangeName;

public record UserChangeNameCommand(Guid id , string name) : IRequest<ErrorOr<Success>>;