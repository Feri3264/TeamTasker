﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Tasker.Application.Session.Command.ChangeName;
using Tasker.Application.Session.Command.Create;
using Tasker.Application.Session.Command.Delete;
using Tasker.Application.Session.Query.GetMySessions;
using Tasker.Contracts.Session.Create;
using Tasker.Contracts.Session.MySessions;

namespace Tasker.Api.Controllers
{
    [Authorize]
    [Route("/api/session")]
    public class SessionController
        (IMediator mediator) : ApiController
    {
        #region Show Sessions By User

        [HttpGet]
        public async Task<IActionResult> MySessions()
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new GetMySessionsQuery(UserId));

            IEnumerable<MySessionsResponseDto> sessions =
                new List<MySessionsResponseDto>();

            if (result.Value is not null)
            {
                sessions = result.Value.Select(r => new MySessionsResponseDto(
                    r.Id,
                    r.Name,
                    r.OwnerId)).ToList();
            }


            return result.Match(
                _ => Ok(sessions), Problem);
        }

        #endregion

        #region Create

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSessionRequestDto request)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new CreateSessionCommand(request.name, UserId));

            return result.Match(session => CreatedAtAction(
                nameof(MySessions),
                new { userId = UserId },
                new CreateSessionResponseDto(
                    session.Id,
                    session.Name,
                    session.OwnerId)) , Problem);
        }


        #endregion

        #region Delete

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var result = await mediator.Send
                (new DeleteSessionCommand(Id));

            return result.Match(
                _ => Ok(), Problem);
        }


        #endregion

        #region ChangeName

        [HttpPatch("{sessionId:guid}/{name}")]
        public async Task<IActionResult> ChangeName([FromRoute] Guid sessionId, [FromRoute] string name)
        {
            var result = await mediator.Send
                (new SessionChangeNameCommand(sessionId, name));

            return result.Match(
                _ => Ok(), Problem);
        }
        

        #endregion
    }
}
