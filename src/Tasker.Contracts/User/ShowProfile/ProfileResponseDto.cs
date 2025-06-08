using System.Globalization;

namespace Tasker.Contracts.User.ShowProfile;

public record ProfileResponseDto(
    Guid userId,
    string name,
    string email,
    string password);