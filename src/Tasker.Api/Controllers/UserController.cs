using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using Tasker.Application.Common.Interfaces.Auth;
using Tasker.Application.User.Command.ChangeName;
using Tasker.Application.User.Command.ChangePassword;
using Tasker.Application.User.Command.Delete;
using Tasker.Application.User.Command.Login;
using Tasker.Application.User.Command.Register;
using Tasker.Application.User.Query.GetMyProfile;
using Tasker.Application.User.Query.SearchUserByEmail;
using Tasker.Contracts.User;
using Tasker.Contracts.User.ChangePassword;
using Tasker.Contracts.User.Login;
using Tasker.Contracts.User.Register;
using Tasker.Contracts.User.SearchByEmail;
using Tasker.Contracts.User.ShowProfile;

namespace Tasker.Api.Controllers
{
    [Authorize]
    [Route("/api/user")]
    public class UserController
        (IMediator mediator,
        IJwtService jwtService) : ApiController
    {

        #region Register
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await mediator.Send
                (new RegisterCommand(request.name, request.email, request.password));

            var jwtToken = jwtService.GenerateToken(
                result.Value.Id,
                result.Value.Email);

            return result.Match(
                user => CreatedAtAction(
                nameof(ShowProfile), 
                new { Id = user.Id },
                    new RegisterResponseDto(
                        user.Id,
                        user.Name,
                        user.Email,
                        user.Password,
                        user.RefreshToken,
                        jwtToken,
                        user.IsDelete)), Problem);
        }

        #endregion

        #region Login

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await mediator.Send
                (new LoginCommand(request.email, request.password));

            var jwtToken = jwtService.GenerateToken(
                result.Value.Id,
                result.Value.Email);

            return result.Match(
                user => CreatedAtAction(
                nameof(ShowProfile),
                new { Id = user.Id },
                new LoginResponseDto(
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Password,
                    user.RefreshToken,
                    jwtToken)), Problem);
        }

        #endregion

        #region ShowProfile

        [HttpGet]
        public async Task<IActionResult> ShowProfile()
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new GetMyProfileQuery(UserId));

            return result.Match(
                user => Ok(
                    new ProfileResponseDto(
                        user.Id,
                        user.Name,
                        user.Email,
                        user.Password)), Problem);
        }

        #endregion

        #region Delete

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new DeleteUserCommand(UserId));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion

        #region Find By Email

        [HttpGet("{email}")]
        public async Task<IActionResult> SearchByEmail([FromRoute] string email)
        {
            var result = await mediator.Send
                (new SearchUserByEmailQuery(email));

            return result.Match(
                user => Ok(
                    new SearchByEmailResponseDto(
                        user.Id,
                        user.Name,
                        user.Email)), Problem);
        }

        #endregion

        #region ChangeName

        [HttpPatch("{name}")]
        public async Task<IActionResult> ChangeName([FromRoute] string name)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new UserChangeNameCommand(UserId, name));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion

        #region ChangePassword

        [HttpPatch]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordRequestDto request)
        {
            if (!TryGetUserId(out Guid UserId))
                return Unauthorized();

            var result = await mediator.Send
                (new UserChangePasswordCommand(UserId, request.oldPassword , request.newPassword));

            return result.Match(
                _ => Ok(), Problem);
        }


        #endregion
    }
}
