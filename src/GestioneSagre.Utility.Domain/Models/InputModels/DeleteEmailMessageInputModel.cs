using GestioneSagre.Shared.Entities;

namespace GestioneSagre.Utility.Domain.Models.InputModels;

public class DeleteEmailMessageInputModel : BaseEntity
{
    public Guid EmailId { get; set; }

    public DeleteEmailMessageInputModel(int id, Guid emailId)
    {
        Id = id;
        EmailId = emailId;
    }
}