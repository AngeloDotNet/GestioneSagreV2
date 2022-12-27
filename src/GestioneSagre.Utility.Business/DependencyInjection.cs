using GestioneSagre.Tools.RabbitMQ;
using GestioneSagre.Utility.Domain.Models.InputModels;
using GestioneSagre.Utility.Domain.Services.Read;
using GestioneSagre.Utility.Domain.UnitOfWork;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Repository.Read;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestioneSagre.Utility.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        services.AddScoped<DbContext, DataDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssemblyOf<UtilityReadRepository>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.Scan(scan =>
            scan.FromAssemblyOf<SendEmailReadService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }

    public static IServiceCollection AddRabbitMqService(this IServiceCollection services, string connectionString,
        string exchangeName, ushort queuePrefetchCount)
    {
        services.AddRabbitMq(settings =>
        {
            settings.ConnectionString = connectionString;
            settings.ExchangeName = exchangeName;
            settings.QueuePrefetchCount = queuePrefetchCount;
        },
        queues =>
        {
            queues.Add<CreateEmailMessageInputModel>();
        });

        return services;
    }
}