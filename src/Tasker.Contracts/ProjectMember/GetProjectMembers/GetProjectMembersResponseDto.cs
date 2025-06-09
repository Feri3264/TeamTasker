namespace Tasker.Contracts.ProjectMember.GetProjectMembers;

public record GetProjectMembersResponseDto(
    Guid userId,
    string name,
    string email);