using AutoMapper;
using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Domain.UnitOfWork;

namespace GestioneSagre.Utility.Domain.Services.Read;

public class SendEmailReadService : ISendEmailReadService
{
    public IUnitOfWork unitOfWork;

    private readonly IMapper mapper;

    public SendEmailReadService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<List<EmailMessageViewModel>> GetAllEmailMessagesAsync()
    {
        var dataLinq = await unitOfWork.UtilityRead.GetAllAsync();

        var result = mapper.Map<List<EmailMessageViewModel>>(dataLinq);

        return result;
    }

    public async Task<EmailMessageViewModel> GetEmailMessageAsync(int id)
    {
        var dataLinq = await unitOfWork.UtilityRead.GetByIdAsync(id);

        var result = mapper.Map<EmailMessageViewModel>(dataLinq);

        return result;
    }
}