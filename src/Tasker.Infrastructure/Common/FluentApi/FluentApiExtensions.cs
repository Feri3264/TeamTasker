using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasker.Infrastructure.Common.FluentApi;

public static class FluentApiExtensions
{
    public static PropertyBuilder<T> HasListOfIdsConverter<T>(this PropertyBuilder<T> builder)
    {
        return builder.HasConversion(
            new ListOfIdsConverter());
    }
}