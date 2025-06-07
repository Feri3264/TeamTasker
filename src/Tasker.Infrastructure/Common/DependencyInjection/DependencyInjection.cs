using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasker.Infrastructure.Common.Context;

namespace Tasker.Infrastructure.Common.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<TeamTaskerDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("TeamTasker"));
        });

        return services;
    }
}
