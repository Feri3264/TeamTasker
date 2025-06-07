using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasker.Domain.User;
using Tasker.Infrastructure.Common.FluentApi;

namespace Tasker.Infrastructure.User;

public class UserConfig : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        //properties
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.Property(u => u.Name).HasMaxLength(70).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(150).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(150).IsRequired();
        builder.Property(u => u.IsDelete).IsRequired();
        builder.Property(u => u.TaskIds).HasListOfIdsConverter();
    }
}