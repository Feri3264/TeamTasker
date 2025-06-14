﻿using ErrorOr;
using MediatR;
using Tasker.Domain.Team;

namespace Tasker.Application.Team.Command.Create;

public record CreateTeamCommand
    (string name , Guid sessionId , Guid leadId) : IRequest<ErrorOr<TeamModel>>;