using GestioneSagre.Utility.Domain.Models.ViewModels;
using MediatR;

namespace GestioneSagre.Utility.QueryStack;

public class GetEmailMessageByIdQuery : IRequest<EmailMessageViewModel>
{
    public int Id { get; set; }

    public GetEmailMessageByIdQuery(int id)
    {
        Id = id;
    }
}