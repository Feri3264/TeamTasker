namespace Tasker.Contracts.Auth.RefreshToken;

public record RefreshTokenResponseDto(
    string jwt,
    string refreshToken);