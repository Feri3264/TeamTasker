using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        //lists
        builder.Property(typeof(List<Guid>) , "_taskIds")
            .HasColumnName("TaskId")
            .HasConversion(new ListOfIdsConverter());

        //navigation
        builder.HasOne<TeamModel>()
            .WithMany()
            .HasForeignKey(p => p.TeamId);
    }
}