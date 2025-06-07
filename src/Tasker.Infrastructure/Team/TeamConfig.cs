using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasker.Domain.Session;
using Tasker.Domain.Team;
using Tasker.Infrastructure.Common.FluentApi;

namespace Tasker.Infrastructure.Team;

public class TeamConfig : IEntityTypeConfiguration<TeamModel>
{
    public void Configure(EntityTypeBuilder<TeamModel> builder)
    {
        //properties
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();

        builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
        builder.Property(t => t.LeadId).ValueGeneratedNever().IsRequired();
        builder.Property(t => t.SessionId).ValueGeneratedNever().IsRequired();
        builder.Property(t => t.ProjectIds).HasListOfIdsConverter().IsRequired();


        //navigation
        builder.HasOne<SessionModel>()
            .WithMany()
            .HasForeignKey(t => t.SessionId);
    }
}