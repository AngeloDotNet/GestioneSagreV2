using GestioneSagre.Shared.RabbitMQ.InputModels;
using GestioneSagre.Tools.MailKit;
using GestioneSagre.Tools.RabbitMQ.Abstractions;
using GestioneSagre.Utility.Web.Api.Internal.Services;

namespace GestioneSagre.Utility.WorkerServices.Receivers;

public class EmailMessageReceiver : IMessageReceiver<EmailMessageInputModel>
{
    private readonly ILogger logger;
    private readonly IEmailClient emailClient;
    private readonly ISendEmailServices sendEmailServices;

    public EmailMessageReceiver(ILogger<EmailMessageReceiver> logger, IEmailClient emailClient, ISendEmailServices sendEmailServices)
    {
        this.logger = logger;
        this.emailClient = emailClient;
        this.sendEmailServices = sendEmailServices;
    }

    public async Task ReceiveAsync(EmailMessageInputModel message, CancellationToken cancellationToken)
    {
        var result = await emailClient.SendEmailAsync(message.RecipientEmail, null, message.Subject, message.Message, cancellationToken);

        if (!result)
        {
            var messageDetail = await sendEmailServices.GetEmailMessageAsync(message.EmailId);

            await sendEmailServices.UpdateEmailStatusAsync(messageDetail.Id, messageDetail.EmailId, 3);
        }
        else
        {
            var messageDetail = await sendEmailServices.GetEmailMessageAsync(message.EmailId);

            await sendEmailServices.UpdateEmailStatusAsync(messageDetail.Id, messageDetail.EmailId, 1);
        }
    }
}