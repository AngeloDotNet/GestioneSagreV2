using GestioneSagre.Utility.CommandStack;
using GestioneSagre.Utility.Domain.Services.Write;
using GestioneSagre.Utility.Infrastructure.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Utility.Business.Handlers.Write;

public class UpdateEmailMessageHandler : IRequestHandler<UpdateEmailMessageCommand, bool>
{
    private readonly ISendEmailWriteService sendEmailWriteService;
    private readonly ILogger logger;

    public UpdateEmailMessageHandler(ISendEmailWriteService sendEmailWriteService, ILogger<UpdateEmailMessageHandler> logger)
    {
        this.sendEmailWriteService = sendEmailWriteService;
        this.logger = logger;
    }

    public async Task<bool> Handle(UpdateEmailMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new EmailMessage(request.Id, request.EmailId, request.Recipient, request.RecipientEmail,
            request.Subject, request.Message, request.SendDate, request.EffectiveSendDate, request.EmailSendCount,
            request.Status);

        logger.LogInformation("Updating record for email {email}", request.EmailId);
        var result = await sendEmailWriteService.UpdateEmailMessage(entity);

        if (!result)
        {
            logger.LogWarning("Error updating record for email {email}", request.EmailId);
            return false;
        }

        return await Task.FromResult(true);
    }
}