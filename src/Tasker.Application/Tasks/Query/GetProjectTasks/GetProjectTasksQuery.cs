using ErrorOr;
using MediatR;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Tasks.Query.GetProjectTasks;

public record GetProjectTasksQuery(Guid projectId) : IRequest<ErrorOr<List<TaskModel>>>;