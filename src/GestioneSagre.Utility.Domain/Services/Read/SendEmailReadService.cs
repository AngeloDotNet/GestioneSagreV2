using AutoMapper;
using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Domain.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace GestioneSagre.Utility.Domain.Services.Read;

public class SendEmailReadService : ISendEmailReadService
{
    public IUnitOfWork unitOfWork;

    private readonly IMapper mapper;
    private readonly ILogger logger;

    public SendEmailReadService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SendEmailReadService> logger)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<List<EmailMessageViewModel>> GetAllEmailMessagesAsync()
    {
        try
        {
            logger.LogInformation("Email detail search");
            var dataLinq = await unitOfWork.UtilityRead.GetAllAsync();

            logger.LogInformation("Mapping data");
            var result = mapper.Map<List<EmailMessageViewModel>>(dataLinq);

            return result;
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Error retrieving all email messages");
            return null;
        }
    }

    public async Task<EmailMessageViewModel> GetEmailMessageAsync(int id)
    {
        try
        {
            logger.LogInformation("Email detail search with id {id}", id);
            var dataLinq = await unitOfWork.UtilityRead.GetByIdAsync(id);

            logger.LogInformation("Mapping data");
            var result = mapper.Map<EmailMessageViewModel>(dataLinq);

            return result;
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Error retrieving email message with id {id}", id);
            return null;
        }
    }
}