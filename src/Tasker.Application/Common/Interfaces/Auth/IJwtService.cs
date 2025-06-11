using System;

namespace Tasker.Application.Common.Interfaces.Auth;

public interface IJwtService
{
    public string GenerateToken(Guid? userId , string? email);
}
