using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Domain.Services.Read;
using GestioneSagre.Utility.QueryStack;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Utility.Business.Handlers.Read;

public class GetEmailMessagesListHandler : IRequestHandler<GetEmailMessagesListQuery, List<EmailMessageViewModel>>
{
    private readonly ISendEmailReadService sendEmailReadService;
    private readonly ILogger logger;

    public GetEmailMessagesListHandler(ISendEmailReadService sendEmailReadService, ILogger<GetEmailMessagesListHandler> logger)
    {
        this.sendEmailReadService = sendEmailReadService;
        this.logger = logger;
    }

    public async Task<List<EmailMessageViewModel>> Handle(GetEmailMessagesListQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting email messages list");
        var result = await sendEmailReadService.GetAllEmailMessagesAsync();

        if (result == null)
        {
            logger.LogWarning("Error getting email messages list");
            return null;
        }

        return await Task.FromResult(result);
    }
}