using System;

namespace Tasker.Application.Common.Interfaces.Auth;

public interface IRefreshTokenService
{
    public string GenerateRefreshToken();
}
