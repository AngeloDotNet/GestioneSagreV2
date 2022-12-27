﻿using GestioneSagre.Shared.RabbitMQ.InputModels;
using GestioneSagre.Tools.MailKit.Options;
using GestioneSagre.Tools.RabbitMQ.Abstractions;
using GestioneSagre.Utility.Web.Api.Internal.Services;
using Microsoft.Extensions.Options;

namespace GestioneSagre.Utility.Web.Api.Internal.HostedServices;

public class EmailSenderHostedService : BackgroundService
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly ILogger logger;
    private readonly IOptionsMonitor<SmtpOptions> smtpOptions;
    private readonly IMessageSender messageSender;

    public EmailSenderHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<EmailSenderHostedService> logger,
        IOptionsMonitor<SmtpOptions> smtpOptions, IMessageSender messageSender)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.logger = logger;
        this.smtpOptions = smtpOptions;
        this.messageSender = messageSender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (IServiceScope serviceScope = serviceScopeFactory.CreateScope())
                {
                    IServiceProvider serviceProvider = serviceScope.ServiceProvider;
                    ISendEmailServices sendEmailServices = serviceProvider.GetRequiredService<ISendEmailServices>();

                    var options = this.smtpOptions.CurrentValue;
                    var emailList = await sendEmailServices.GetAllEmailMessagesAsync();
                    var timer = (int)TimeSpan.FromSeconds(options.TimerInSeconds).TotalMilliseconds;

                    if (emailList.Count != 0)
                    {
                        foreach (var email in emailList)
                        {
                            if (email.EmailSendCount >= options.MaxSenderCount)
                            {
                                await sendEmailServices.UpdateEmailStatusAsync(email.Id, email.EmailId, 2);
                            }
                            else
                            {
                                EmailMessageInputModel message = new()
                                {
                                    Id = email.Id,
                                    EmailId = email.EmailId,
                                    RecipientEmail = email.RecipientEmail,
                                    ReplyEmail = null,
                                    Subject = email.Subject,
                                    Message = email.Message
                                };

                                await messageSender.PublishAsync(message);
                            }

                            Thread.Sleep(timer);
                        }
                    }

                    Thread.Sleep(timer);
                }
            }
            catch (Exception exc)
            {
                if (!stoppingToken.IsCancellationRequested)
                {
                    logger.LogError(exc, "Error sending email");
                }
            }
        }
    }
}