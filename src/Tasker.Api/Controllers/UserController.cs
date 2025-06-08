using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    [Route("/api/user")]
    public class UserController
        (IMediator mediator) : ApiController
    {

        #region Register

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await mediator.Send
                (new RegisterCommand(request.name, request.email, request.password));

            return result.Match(
                user => CreatedAtAction(
                nameof(ShowProfile), 
                new { Id = user.Id },
                    new RegisterResponseDto(
                        user.Id,
                        user.Name,
                        user.Email,
                        user.Password,
                        user.IsDelete)), Problem);
        }

        #endregion

        #region Login

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await mediator.Send
                (new LoginCommand(request.email, request.password));

            return result.Match(
                user => CreatedAtAction(
                nameof(ShowProfile),
                new { Id = user.Id },
                new LoginResponseDto(
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Password)), Problem);
        }

        #endregion

        #region ShowProfile

        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> ShowProfile([FromRoute] Guid Id)
        {
            var result = await mediator.Send
                (new GetMyProfileQuery(Id));

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

        [HttpDelete("{Id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var result = await mediator.Send
                (new DeleteUserCommand(Id));

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

        [HttpPatch("{Id:guid}/{name}")]
        public async Task<IActionResult> ChangeName([FromRoute] Guid Id , [FromRoute] string name)
        {
            var result = await mediator.Send
                (new UserChangeNameCommand(Id, name));

            return result.Match(
                _ => Ok(), Problem);
        }

        #endregion

        #region ChangePassword

        [HttpPatch("{Id:guid}")]
        public async Task<IActionResult> ChangePassword(
            [FromRoute] Guid Id,
            [FromBody] UserChangePasswordRequestDto request)
        {
            var result = await mediator.Send
                (new UserChangePasswordCommand(Id, request.oldPassword , request.newPassword));

            return result.Match(
                _ => Ok(), Problem);
        }


        #endregion
    }
}
