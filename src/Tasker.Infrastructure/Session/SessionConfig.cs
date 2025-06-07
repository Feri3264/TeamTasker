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

        //lists
        builder.Property(typeof(List<Guid>), "_editors")
            .HasColumnName("Editors")
            .HasConversion(new ListOfIdsConverter());

        builder.Property(typeof(List<Guid>), "_teamIds")
            .HasColumnName("TeamIds")
            .HasConversion(new ListOfIdsConverter());

        //navigation
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(s => s.OwnerId);
    }
}