using GestioneSagre.Tools.MailKit;
using GestioneSagre.Tools.MailKit.Options;
using GestioneSagre.Utility.Web.Api.Internal.Services;
using Microsoft.Extensions.Options;

namespace GestioneSagre.Utility.Web.Api.Internal.HostedServices;

public class EmailSenderHostedService : BackgroundService
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly ILogger logger;
    private readonly IEmailClient emailClient;
    private readonly IOptionsMonitor<SmtpOptions> smtpOptions;

    public EmailSenderHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<EmailSenderHostedService> logger,
        IEmailClient emailClient, IOptionsMonitor<SmtpOptions> smtpOptions)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.logger = logger;
        this.emailClient = emailClient;
        this.smtpOptions = smtpOptions;
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

                    //Vengono filtrati i record con status PENDING. Gli status FAILED e SENT non vengono presi in considerazione
                    var emailList = await sendEmailServices.GetAllEmailMessagesAsync();

                    var timerWaiting = (int)TimeSpan.FromSeconds(options.TimerInSeconds * 3).TotalMilliseconds;
                    var timerForeach = (int)TimeSpan.FromSeconds(options.TimerInSeconds).TotalMilliseconds;

                    if (emailList.Count != 0)
                    {
                        foreach (var email in emailList)
                        {
                            if (email.EmailSendCount >= options.MaxSenderCount)
                            {
                                //Se EmailSendCount è maggiore/uguale di MaxSenderCount, imposto status a FAILED
                                await sendEmailServices.UpdateEmailStatusAsync(email.Id, email.EmailId, 2);
                            }
                            else
                            {
                                //Se status PENDING e EmailSendCount minore di MaxSenderCount eseguo l'invio dell'email
                                var result = await emailClient.SendEmailAsync(email.RecipientEmail, null, email.Subject, email.Message, stoppingToken);

                                if (!result)
                                {
                                    // Se FALSE la mail non è stata spedita lo stato rimane PENDING e EmailSendCount +1
                                    await sendEmailServices.UpdateEmailStatusAsync(email.Id, email.EmailId, 3);
                                }
                                else
                                {
                                    // Se TRUE la mail è stata spedita, lo stato diventa SENT
                                    await sendEmailServices.UpdateEmailStatusAsync(email.Id, email.EmailId, 1);
                                }
                            }

                            Thread.Sleep(timerForeach);
                        }
                    }

                    Thread.Sleep(timerWaiting);
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