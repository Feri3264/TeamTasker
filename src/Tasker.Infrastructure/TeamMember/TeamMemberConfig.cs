using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasker.Domain.Team;
using Tasker.Domain.TeamMember;
using Tasker.Domain.User;

namespace Tasker.Infrastructure.TeamMember;

public class TeamMemberConfig : IEntityTypeConfiguration<TeamMemberModel>
{
    public void Configure(EntityTypeBuilder<TeamMemberModel> builder)
    {
        //properties
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();

        builder.Property(t => t.UserId).ValueGeneratedNever().IsRequired();
        builder.Property(t => t.TeamId).ValueGeneratedNever().IsRequired();

        //navigation
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(t => t.UserId);

        builder.HasOne<TeamModel>()
            .WithMany()
            .HasForeignKey(t => t.TeamId);
    }
}