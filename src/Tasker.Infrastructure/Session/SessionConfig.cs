using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasker.Domain.Session;
using Tasker.Domain.User;
using Tasker.Infrastructure.Common.FluentApi;

namespace Tasker.Infrastructure.Session;

public class SessionConfig : IEntityTypeConfiguration<SessionModel>
{
    public void Configure(EntityTypeBuilder<SessionModel> builder)
    {
        //properties
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();

        builder.Property(s => s.Name).HasMaxLength(50).IsRequired();
        builder.Property(s => s.OwnerId).ValueGeneratedNever().IsRequired();
        builder.Property(s => s.Editors).HasListOfIdsConverter().IsRequired();
        builder.Property(s => s.TeamIds).HasListOfIdsConverter().IsRequired();

        //navigation
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(s => s.OwnerId);
    }
}