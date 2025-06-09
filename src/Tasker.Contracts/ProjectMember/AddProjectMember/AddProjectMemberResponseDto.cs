namespace Tasker.Contracts.ProjectMember.AddProjectMember;

public record AddProjectMemberResponseDto(
    Guid Id,
    Guid projectId,
    Guid userId);