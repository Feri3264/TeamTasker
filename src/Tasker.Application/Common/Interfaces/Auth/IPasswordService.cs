namespace Tasker.Application.Common.Interfaces.Auth;

public interface IPasswordService
{
    public string HashPassword(string password);
}