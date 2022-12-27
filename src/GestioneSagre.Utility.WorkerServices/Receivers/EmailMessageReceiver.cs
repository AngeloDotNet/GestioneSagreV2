using System.Net;
using System.Net.Http.Json;
using GestioneSagre.Shared.Models.InputModels;
using GestioneSagre.Tools.RabbitMQ.Abstractions;
using GestioneSagre.Utility.Domain.Models.InputModels;

namespace GestioneSagre.Utility.WorkerServices.Receivers;

public class EmailMessageReceiver : IMessageReceiver<CreateEmailMessageInputModel>
{
    private readonly ILogger logger;
    private readonly HttpClient httpClient;

    public EmailMessageReceiver(ILogger<EmailMessageReceiver> logger, HttpClient httpClient)
    {
        this.logger = logger;
        this.httpClient = httpClient;
    }

    public async Task ReceiveAsync(CreateEmailMessageInputModel message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Send the email to {Recipient} - {RecipientEmail}, with subject {Subject} and message {Message}",
            message.Recipient, message.RecipientEmail, message.Subject, message.Message);

        EmailInputModel emailMessage = new()
        {
            RecipientEmail = message.RecipientEmail,
            ReplyEmail = null,
            Subject = message.Subject,
            Message = message.Message
        };

        var result = await httpClient.PostAsJsonAsync("https://localhost:7228/api/email", emailMessage, cancellationToken);

        if (result.StatusCode != HttpStatusCode.OK)
        {
            //Se esito negativo aggiorno lo stato della mail a Failed
            logger.LogWarning("The sending of the email {EmailId}, failed.", message.EmailId);
        }

        //Se esito positivo aggiorno lo stato della mail a Sent
        logger.LogInformation("Sending the email {EmailId}, has been sent successfully", message.EmailId);
    }
}