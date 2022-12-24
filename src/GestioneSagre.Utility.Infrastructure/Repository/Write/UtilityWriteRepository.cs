using GestioneSagre.Shared.GenericRepository.Write;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Infrastructure.Repository.Write;

public class UtilityWriteRepository : GenericWriteRepository<EmailMessage>, IUtilityWriteRepository
{
    public UtilityWriteRepository(DataDbContext context) : base(context)
    {
    }
}