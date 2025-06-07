using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasker.Domain.Project;
using Tasker.Domain.ProjectMember;
using Tasker.Domain.User;

namespace Tasker.Infrastructure.ProjectMember;

public class ProjectMemberConfig : IEntityTypeConfiguration<ProjectMemberModel>
{
    public void Configure(EntityTypeBuilder<ProjectMemberModel> builder)
    {
        //properties
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.Property(p => p.UserId).ValueGeneratedNever().IsRequired();
        builder.Property(p => p.ProjectId).ValueGeneratedNever().IsRequired();

        //navigation
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(p => p.UserId);

        builder.HasOne<ProjectModel>()
            .WithMany()
            .HasForeignKey(p => p.ProjectId);
    }
}