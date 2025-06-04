using ErrorOr;
using MediatR;
using Tasker.Domain.Tasks;

namespace Tasker.Application.Tasks.Query.GetMyTasks;

public record GetMyTasksQuery(Guid id) : IRequest<ErrorOr<List<TaskModel>>>;