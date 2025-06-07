using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasker.Application.Common.Interfaces.Auth;
using Tasker.Application.Common.Interfaces.Repositories;
using Tasker.Infrastructure.Common.Auth;
using Tasker.Infrastructure.Common.Context;
using Tasker.Infrastructure.Project;
using Tasker.Infrastructure.ProjectMember;
using Tasker.Infrastructure.Session;
using Tasker.Infrastructure.SessionMember;
using Tasker.Infrastructure.Tasks;
using Tasker.Infrastructure.Team;
using Tasker.Infrastructure.TeamMember;
using Tasker.Infrastructure.User;

namespace Tasker.Infrastructure.Common.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration configuration)
    {

        //context
        services.AddDbContext<TeamTaskerDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("TeamTasker"));
        });

        services.AddScoped<TeamTaskerDbContext>();


        //repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<ISessionMemberRepository, SessionMemberRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();


        //services
        services.AddScoped<IPasswordService, PasswordService>();

        return services;
    }
}
