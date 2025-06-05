using ErrorOr;
using MediatR;
using Tasker.Domain.TeamMember;

namespace Tasker.Application.TeamMember.Command.Create;

public record CreateTeamMemberCommand
    (Guid teamId , Guid userId) : IRequest<ErrorOr<TeamMemberModel>>;