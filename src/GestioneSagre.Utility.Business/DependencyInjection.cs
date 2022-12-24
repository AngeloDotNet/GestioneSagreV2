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
        //services.AddScoped<IUtilityReadRepository, UtilityReadRepository>();
        //services.AddScoped<IUtilityWriteRepository, UtilityWriteRepository>();

        services.Scan(scan =>
            scan.FromAssemblyOf<UtilityReadRepository>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        //services.AddTransient<ISendEmailReadService, SendEmailReadService>();
        //services.AddTransient<ISendEmailWriteService, SendEmailWriteService>();

        services.Scan(scan =>
            scan.FromAssemblyOf<SendEmailReadService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return services;
    }
}