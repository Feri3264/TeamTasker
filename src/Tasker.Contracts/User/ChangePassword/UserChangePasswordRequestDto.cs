namespace Tasker.Contracts.User.ChangePassword;

public record UserChangePasswordRequestDto(string oldPassword , string newPassword);