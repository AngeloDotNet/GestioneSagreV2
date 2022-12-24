namespace GestioneSagre.Utility.Domain.Models.InputModels;

public class DeleteEmailMessageInputModel
{
    public int Id { get; set; }
    public Guid EmailId { get; set; }

    public DeleteEmailMessageInputModel(int id, Guid emailId)
    {
        Id = id;
        EmailId = emailId;
    }
}