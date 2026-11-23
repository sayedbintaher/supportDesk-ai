using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportDeskAI.Application.Interfaces;
using SupportDeskAI.Infrastructure.Persistence;
using SupportDeskAI.Infrastructure.Persistence.Repositories;


public static class DependencyInjection
{
    public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Database connection string is missing.");
        }

        services.AddDbContext<SupportDeskDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<ITicketRepository, TicketRepository>();
        return services;
    }
}