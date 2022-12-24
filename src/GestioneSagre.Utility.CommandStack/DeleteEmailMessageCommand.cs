using GestioneSagre.Utility.Domain.Models.InputModels;
using MediatR;

namespace GestioneSagre.Utility.CommandStack;
public class DeleteEmailMessageCommand : IRequest<bool>
{
    public int Id { get; set; }
    public Guid EmailId { get; set; }

    public DeleteEmailMessageCommand(DeleteEmailMessageInputModel inputModel)
    {
        Id = inputModel.Id;
        EmailId = inputModel.EmailId;
    }
}