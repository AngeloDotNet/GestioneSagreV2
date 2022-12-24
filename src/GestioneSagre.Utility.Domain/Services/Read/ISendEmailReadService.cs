using GestioneSagre.Utility.Domain.Models.ViewModels;

namespace GestioneSagre.Utility.Domain.Services.Read;

public interface ISendEmailReadService
{
    Task<List<EmailMessageViewModel>> GetAllEmailMessagesAsync();
    Task<EmailMessageViewModel> GetEmailMessageAsync(int id);
}