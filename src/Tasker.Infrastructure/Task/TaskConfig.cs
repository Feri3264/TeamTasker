using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasker.Domain.Project;
using Tasker.Domain.Tasks;
using Tasker.Domain.User;

namespace Tasker.Infrastructure.Task;

public class TaskConfig : IEntityTypeConfiguration<TaskModel>
{
    public void Configure(EntityTypeBuilder<TaskModel> builder)
    {
        //properties
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();

        builder.Property(t => t.Name).HasMaxLength(100).IsRequired();
        builder.Property(t => t.Status).IsRequired();
        builder.Property(t => t.Priority).IsRequired();
        builder.Property(t => t.AssignedMemberId).ValueGeneratedNever().IsRequired();
        builder.Property(t => t.ProjectId).ValueGeneratedNever().IsRequired();

        builder.Property(t => t.Deadline).HasColumnType("timestamp without time zone")
            .IsRequired();


        //navigation
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(t => t.AssignedMemberId);

        builder.HasOne<ProjectModel>()
            .WithMany()
            .HasForeignKey(t => t.ProjectId);

    }
}