using GestioneSagre.Tools.MailKit;
using GestioneSagre.Utility.Core;
using GestioneSagre.Utility.Domain.Services.Read;
using GestioneSagre.Utility.Domain.UnitOfWork;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Repository.Read;
using Microsoft.AspNetCore.Identity.UI.Services;
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

    public static IServiceCollection AddWorkerService(this IServiceCollection services)
    {
        services.AddTransient<ISendEmailServices, SendEmailServices>();

        services.AddSingleton<IEmailSender, MailKitEmailSender>();
        services.AddSingleton<IEmailClient, MailKitEmailSender>();

        return services;
    }
}