using System.ComponentModel.DataAnnotations;

namespace GestioneSagre.Shared.Entities;

public abstract class BaseEntity
{
    [Required(ErrorMessage = "Id is required")] public int Id { get; set; }
}