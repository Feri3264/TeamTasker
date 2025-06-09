using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tasker.Application.SessionMember.Command.Create;
using Tasker.Application.SessionMember.Command.Delete;
using Tasker.Application.SessionMember.Query.GetSessionMembers;
using Tasker.Contracts.SessionMember.AddSessionMember;
using Tasker.Contracts.SessionMember.GetSessionMembers;

namespace Tasker.Api.Controllers
{
    [Route("/api/session")]
    public class SessionMemberController
        (IMediator mediator) : ApiController
    {

        #region Show Members In Session

        [HttpGet("{sessionId:guid}/member")]
        public async Task<IActionResult> GetMembers([FromRoute] Guid sessionId)
        {
            var result = await mediator.Send
                (new GetSessionMembersQuery(sessionId));

            IEnumerable<GetSessionMembersResponseDto> members = 
                    new List<GetSessionMembersResponseDto>();

            if (result.Value is not null)
            {
                members = result.Value.Select(r => new GetSessionMembersResponseDto(
                    r.Id,
                    r.Name,
                    r.Email)).ToList();
            }


            return result.Match(
                _ => Ok(members), Problem);
        }

        #endregion

        #region Add Member To Session

        [HttpPost("{sessionId:guid}/member")]
        public async Task<IActionResult> AddMember(
            [FromRoute] Guid sessionId,
            [FromBody] AddSessionMemberRequestDto request)
        {
            var result = await mediator.Send
                (new CreateSessionMemberCommand(sessionId, request.userId));

            return result.Match(
                sessionMember => CreatedAtAction(
                    nameof(GetMembers),
                    new { sessionId = sessionMember.SessionId },
                    new AddSessionMemberResponseDto(
                        sessionMember.Id,
                        sessionMember.SessionId,
                        sessionMember.UserId)), Problem);
        }

        #endregion

        #region Remove Member From Session

        [HttpDelete("{sessionId}/member/{memberId}")]
        public async Task<IActionResult> RemoveMember(
            [FromRoute] Guid sessionId,
            [FromRoute] Guid memberId)
        {
            var result = await mediator.Send
                (new DeleteSessionMemberCommand(memberId, sessionId));

            return result.Match(
                _ => Ok(), Problem);
        }


        #endregion
    }
}
