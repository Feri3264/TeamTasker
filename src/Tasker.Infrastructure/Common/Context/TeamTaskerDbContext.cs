using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tasker.Domain.Project;
using Tasker.Domain.ProjectMember;
using Tasker.Domain.Session;
using Tasker.Domain.SessionMember;
using Tasker.Domain.Tasks;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Infrastructure.Common.Context;

public class TeamTaskerDbContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<SessionModel> Sessions { get; set; }
    public DbSet<SessionMemberModel> SessionMembers { get; set; }
    public DbSet<TeamModel> Teams { get; set; }
    public DbSet<TeamMemberModel> TeamMembers { get; set; }
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<ProjectMemberModel> ProjectMembers { get; set; }
    public DbSet<TaskModel> Tasks { get; set; }


    public TeamTaskerDbContext(DbContextOptions<TeamTaskerDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}