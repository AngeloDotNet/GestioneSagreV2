using GestioneSagre.Shared.RabbitMQ.InputModels;
using GestioneSagre.Tools.MailKit;
using GestioneSagre.Tools.MailKit.Options;
using GestioneSagre.Tools.RabbitMQ;
using GestioneSagre.Utility.Core;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.WorkerServices.Receivers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

await host.RunAsync();

void ConfigureServices(HostBuilderContext hostingContext, IServiceCollection services)
{
    var configuration = hostingContext.Configuration;

    services.AddRabbitMq(settings =>
    {
        settings.ConnectionString = configuration.GetConnectionString("RabbitMQ");
        settings.ExchangeName = configuration.GetValue<string>("RabbitMqSettings:ApplicationName");
        settings.QueuePrefetchCount = configuration.GetValue<ushort>("RabbitMqSettings:QueuePrefetchCount");
    }, queues =>
    {
        queues.Add<EmailMessageInputModel>();
    })
   .AddReceiver<EmailMessageInputModel, EmailMessageReceiver>();

    services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(configuration.GetSection("ConnectionStrings").GetValue<string>("Database"),
                migration => migration.MigrationsAssembly("GestioneSagre.Utility.Infrastructure"))
    );

    services.AddTransient<ISendEmailServices, SendEmailServices>();

    services.AddSingleton<IEmailSender, MailKitEmailSender>();
    services.AddSingleton<IEmailClient, MailKitEmailSender>();

    services.Configure<SmtpOptions>(configuration.GetSection("Smtp"));
}