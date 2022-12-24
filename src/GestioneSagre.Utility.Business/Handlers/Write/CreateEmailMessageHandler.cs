using AutoMapper;
using GestioneSagre.Utility.CommandStack;
using GestioneSagre.Utility.Domain.Services.Write;
using GestioneSagre.Utility.Infrastructure.Entities;
using MediatR;

namespace GestioneSagre.Utility.Business.Handlers.Write;

public class CreateEmailMessageHandler : IRequestHandler<CreateEmailMessageCommand, bool>
{
    private readonly ISendEmailWriteService sendEmailWriteService;

    public CreateEmailMessageHandler(ISendEmailWriteService sendEmailWriteService, IMapper mapper)
    {
        this.sendEmailWriteService = sendEmailWriteService;
    }

    public async Task<bool> Handle(CreateEmailMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new EmailMessage(0, request.EmailId, request.Recipient, request.RecipientEmail,
            request.Subject, request.Message, request.SendDate, request.EffectiveSendDate, request.EmailSendCount,
            request.Status);

        var result = await sendEmailWriteService.CreateEmailMessage(entity);

        if (!result)
        {
            return false;
        }

        return await Task.FromResult(true);
    }
}
