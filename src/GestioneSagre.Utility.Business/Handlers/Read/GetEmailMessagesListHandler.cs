using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Domain.Services.Read;
using GestioneSagre.Utility.QueryStack;
using MediatR;

namespace GestioneSagre.Utility.Business.Handlers.Read;

public class GetEmailMessagesListHandler : IRequestHandler<GetEmailMessagesListQuery, List<EmailMessageViewModel>>
{
    private readonly ISendEmailReadService sendEmailReadService;

    public GetEmailMessagesListHandler(ISendEmailReadService sendEmailReadService)
    {
        this.sendEmailReadService = sendEmailReadService;
    }

    public async Task<List<EmailMessageViewModel>> Handle(GetEmailMessagesListQuery request, CancellationToken cancellationToken)
    {
        var result = await sendEmailReadService.GetAllEmailMessagesAsync();

        return await Task.FromResult(result);
    }
}