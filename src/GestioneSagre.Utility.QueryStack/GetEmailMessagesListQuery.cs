using GestioneSagre.Utility.Domain.Models.ViewModels;
using MediatR;

namespace GestioneSagre.Utility.QueryStack
{
    public class GetEmailMessagesListQuery : IRequest<List<EmailMessageViewModel>>
    {

    }
}