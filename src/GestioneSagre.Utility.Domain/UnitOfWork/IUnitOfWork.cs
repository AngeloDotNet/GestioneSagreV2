using GestioneSagre.Utility.Infrastructure.Repository.Read;
using GestioneSagre.Utility.Infrastructure.Repository.Write;

namespace GestioneSagre.Utility.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUtilityReadRepository UtilityRead { get; }
    IUtilityWriteRepository UtilityWrite { get; }
}