using GestioneSagre.Shared.RabbitMQ.InputModels;
using GestioneSagre.Tools.MailKit;
using GestioneSagre.Tools.RabbitMQ.Abstractions;
using GestioneSagre.Utility.Core;

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
        try
        {
            logger.LogInformation("Attempt to send email to the email address {email}", message.RecipientEmail);
            var result = await emailClient.SendEmailAsync(message.RecipientEmail, null, message.Subject, message.Message, cancellationToken);

            logger.LogInformation("Retrieving email details {email}", message.EmailId);
            var messageDetail = await sendEmailServices.GetEmailMessageAsync(message.EmailId);

            if (messageDetail != null)
            {
                if (!result)
                {
                    logger.LogWarning("Updated send failure count for email {email}", messageDetail.EmailId);
                    await sendEmailServices.UpdateEmailStatusAsync(messageDetail.Id, messageDetail.EmailId, 3);
                }
                else
                {
                    logger.LogInformation("Updated the effective email send date and the status was set to sent for the email {email}", messageDetail.EmailId);
                    await sendEmailServices.UpdateEmailStatusAsync(messageDetail.Id, messageDetail.EmailId, 1);
                }
            }
            else
            {
                logger.LogWarning("Email details could not be retrieved { email}", message.EmailId);
            }
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Error during automatic process utility worker service for processing email {email}", message.EmailId);
        }
    }
}