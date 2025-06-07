using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasker.Domain.Session;
using Tasker.Domain.SessionMember;
using Tasker.Domain.User;

namespace Tasker.Infrastructure.SessionMember;

public class SessionMemberConfig : IEntityTypeConfiguration<SessionMemberModel>
{
    public void Configure(EntityTypeBuilder<SessionMemberModel> builder)
    {
        //properties
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();

        //navigation
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(s => s.UserId);

        builder.HasOne<SessionModel>()
            .WithMany()
            .HasForeignKey(s => s.SessionId);
    }
}