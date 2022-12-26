using GestioneSagre.Utility.Domain.Models.ViewModels;

namespace GestioneSagre.Utility.Web.Api.Internal.Services;
public interface ISendEmailServices
{
    Task<List<EmailMessageViewModel>> GetAllEmailMessagesAsync();
    Task<bool> UpdateEmailStatusAsync(int id, Guid emailId, int status);
}