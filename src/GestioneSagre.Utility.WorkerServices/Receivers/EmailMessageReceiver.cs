using GestioneSagre.RabbitMQ.Abstractions;
using GestioneSagre.Utility.Domain.Models.InputModels;

namespace GestioneSagre.Utility.WorkerServices.Receivers;

public class EmailMessageReceiver : IMessageReceiver<CreateEmailMessageInputModel>
{
    private readonly ILogger logger;

    public EmailMessageReceiver(ILogger<EmailMessageReceiver> logger)
    {
        this.logger = logger;
    }

    public async Task ReceiveAsync(CreateEmailMessageInputModel message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Send the email to {Recipient} - {RecipientEmail}, with subject {Subject} and message {Message}",
            message.Recipient, message.RecipientEmail, message.Subject, message.Message);

        //Invio l'email ed attendo l'esito dell'invio
        await Task.Delay(TimeSpan.FromSeconds(10 + Random.Shared.Next(10)), cancellationToken);

        //Se esito positivo aggiorno lo stato della mail a Sent
        logger.LogInformation("Sending the email {EmailId}, has been sent successfully", message.EmailId);

        //Se esito negativo aggiorno lo stato della mail a Failed
        logger.LogWarning("The sending of the email {EmailId}, failed.", message.EmailId);
    }
}