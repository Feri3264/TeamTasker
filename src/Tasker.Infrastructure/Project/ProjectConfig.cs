using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasker.Domain.Project;
using Tasker.Domain.Team;
using Tasker.Infrastructure.Common.FluentApi;

namespace Tasker.Infrastructure.Project;

public class ProjectConfig : IEntityTypeConfiguration<ProjectModel>
{
    public void Configure(EntityTypeBuilder<ProjectModel> builder)
    {
        //properties
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        builder.Property(p => p.LeadId).ValueGeneratedNever().IsRequired();
        builder.Property(p => p.TeamId).ValueGeneratedNever().IsRequired();
        builder.Property(p => p.TaskIds).HasListOfIdsConverter().IsRequired();

        //navigation
        builder.HasOne<TeamModel>()
            .WithMany()
            .HasForeignKey(p => p.TeamId);
    }
}