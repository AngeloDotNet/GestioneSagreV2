using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Domain.Services.Read;
using GestioneSagre.Utility.QueryStack;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Utility.Business.Handlers.Read;

public class GetEmailMessageByIdHandler : IRequestHandler<GetEmailMessageByIdQuery, EmailMessageViewModel>
{
    private readonly ISendEmailReadService sendEmailReadService;
    private readonly ILogger logger;

    public GetEmailMessageByIdHandler(ISendEmailReadService sendEmailReadService, ILogger<GetEmailMessageByIdHandler> logger)
    {
        this.sendEmailReadService = sendEmailReadService;
        this.logger = logger;
    }

    public async Task<EmailMessageViewModel> Handle(GetEmailMessageByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting email message {id}", request.Id);
        var result = await sendEmailReadService.GetEmailMessageAsync(request.Id);

        if (result == null)
        {
            logger.LogWarning("Error getting email message {id}", request.Id);
            return null;
        }

        return await Task.FromResult(result);
    }
}