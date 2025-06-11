using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tasker.Application.Common.Interfaces.Auth;
using Tasker.Application.User.Query.GetUserByRefreshToken;
using Tasker.Contracts.Auth.RefreshToken;

namespace Tasker.Api.Controllers
{
    [Authorize]
    [Route("/api/auth")]
    public class AuthController
        (IMediator mediator,
            IJwtService jwtService,
            IRefreshTokenService refreshTokenService) : ApiController
    {
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {

            var user = await mediator.Send
                (new GetUserByRefreshTokenQuery(request.refreshToken));

            if (user.IsError)
                return NotFound("Invalid refresh token");

            if (user.Value.TokenExpire < DateTime.UtcNow)
                return ValidationProblem("Refresh token expired");

            var newJwt = jwtService.GenerateToken(
                user.Value.Id,
                user.Value.Email);

            var response = new RefreshTokenResponseDto(newJwt, request.refreshToken);

            return user.Match(
                _ => Ok(response), Problem);

        }
    }
}
