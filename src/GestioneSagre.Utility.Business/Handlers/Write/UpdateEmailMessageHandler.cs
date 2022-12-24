using GestioneSagre.Utility.CommandStack;
using GestioneSagre.Utility.Domain.Services.Write;
using GestioneSagre.Utility.Infrastructure.Entities;
using MediatR;

namespace GestioneSagre.Utility.Business.Handlers.Write;

public class UpdateEmailMessageHandler : IRequestHandler<UpdateEmailMessageCommand, bool>
{
    private readonly ISendEmailWriteService sendEmailWriteService;

    public UpdateEmailMessageHandler(ISendEmailWriteService sendEmailWriteService)
    {
        this.sendEmailWriteService = sendEmailWriteService;
    }

    public async Task<bool> Handle(UpdateEmailMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new EmailMessage(request.Id, request.EmailId, request.Recipient, request.RecipientEmail,
            request.Subject, request.Message, request.SendDate, request.EffectiveSendDate, request.EmailSendCount,
            request.Status);

        var result = await sendEmailWriteService.UpdateEmailMessage(entity);

        if (!result)
        {
            return false;
        }

        return await Task.FromResult(true);
    }
}