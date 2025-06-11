using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasker.Application.Team.Command.ChangeName;
using Tasker.Application.Team.Command.Create;
using Tasker.Application.Team.Command.Delete;
using Tasker.Application.Team.Query.GetSessionTeams;
using Tasker.Contracts.Team.Create;
using Tasker.Contracts.Team.GetSessionTeams;

namespace Tasker.Api.Controllers
{
    [Authorize]
    [Route("/api/team")]
    public class TeamController
        (IMediator mediator) : ApiController
    {

        #region Show Teams By Session

        [HttpGet("{sessionId:guid}")]
        public async Task<IActionResult> GetSessionTeams([FromRoute] Guid sessionId)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new GetSessionTeamsQuery(sessionId, UserId));

            IEnumerable<GetSessionTeamsResponseDto> teams =
                new List<GetSessionTeamsResponseDto>();

            if (result.Value is not null)
            {
                teams = result.Value.Select(r => new GetSessionTeamsResponseDto(
                    r.Id,
                    r.Name,
                    r.LeadId)).ToList();
            }

            return result.Match(
                _ => Ok(teams), Problem);
        }

        #endregion

        #region Create

        [HttpPost("{sessionId:guid}")]
        public async Task<IActionResult> Create(
            [FromRoute] Guid sessionId,
            [FromBody] CreateTeamRequestDto request)
        {
            if (!TryGetUserId(out Guid CreatorId))
                return Unauthorized();

            var result = await mediator.Send
                (new CreateTeamCommand(request.name, sessionId, request.leadId));

            return result.Match(
                team => CreatedAtAction(
                    nameof(GetSessionTeams),
                    new { sessionId = sessionId, userId = CreatorId },
                    new CreateTeamResponseDto(
                        team.Id,
                        team.Name,
                        team.LeadId)), Problem);
        }

        #endregion

        #region Delete

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var result = await mediator.Send
                (new DeleteTeamCommand(Id));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion

        #region Change Name

        [HttpPatch("{Id:guid}/{name}")]
        public async Task<IActionResult> ChangeName(
            [FromRoute] Guid Id,
            [FromRoute] string name)
        {
            var result = await mediator.Send
                (new TeamChangeNameCommand(Id, name));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion
    }
}
