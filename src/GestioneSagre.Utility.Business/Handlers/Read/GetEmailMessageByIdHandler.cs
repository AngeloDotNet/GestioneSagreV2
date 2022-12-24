using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Domain.Services.Read;
using GestioneSagre.Utility.QueryStack;
using MediatR;

namespace GestioneSagre.Utility.Business.Handlers.Read;

public class GetEmailMessageByIdHandler : IRequestHandler<GetEmailMessageByIdQuery, EmailMessageViewModel>
{
    private readonly ISendEmailReadService sendEmailReadService;

    public GetEmailMessageByIdHandler(ISendEmailReadService sendEmailReadService)
    {
        this.sendEmailReadService = sendEmailReadService;
    }

    public async Task<EmailMessageViewModel> Handle(GetEmailMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await sendEmailReadService.GetEmailMessageAsync(request.Id);

        return await Task.FromResult(result);
    }
}