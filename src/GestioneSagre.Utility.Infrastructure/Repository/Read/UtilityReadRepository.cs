using GestioneSagre.Shared.GenericRepository.Read;
using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Infrastructure.Repository.Read;

public class UtilityReadRepository : GenericReadRepository<EmailMessage>, IUtilityReadRepository
{
    public UtilityReadRepository(DataDbContext context) : base(context)
    {
    }
}