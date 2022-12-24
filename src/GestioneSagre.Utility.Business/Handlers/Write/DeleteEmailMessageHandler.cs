using GestioneSagre.Utility.CommandStack;
using GestioneSagre.Utility.Domain.Services.Write;
using MediatR;

namespace GestioneSagre.Utility.Business.Handlers.Write;

public class DeleteEmailMessageHandler : IRequestHandler<DeleteEmailMessageCommand, bool>
{
    private readonly ISendEmailWriteService sendEmailWriteService;

    public DeleteEmailMessageHandler(ISendEmailWriteService sendEmailWriteService)
    {
        this.sendEmailWriteService = sendEmailWriteService;
    }

    public async Task<bool> Handle(DeleteEmailMessageCommand request, CancellationToken cancellationToken)
    {
        var result = await sendEmailWriteService.DeleteEmailMessage(request.Id, request.EmailId);

        if (!result)
        {
            return false;
        }

        return await Task.FromResult(true);
    }
}