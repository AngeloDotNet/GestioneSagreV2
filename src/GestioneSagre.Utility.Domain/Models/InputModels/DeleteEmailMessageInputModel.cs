using System.ComponentModel.DataAnnotations;
using GestioneSagre.Shared.Entities;

namespace GestioneSagre.Utility.Domain.Models.InputModels;

public class DeleteEmailMessageInputModel : BaseEntity
{
    [Required(ErrorMessage = "EmailId is required")] public Guid EmailId { get; set; }

    public DeleteEmailMessageInputModel(int id, Guid emailId)
    {
        Id = id;
        EmailId = emailId;
    }
}