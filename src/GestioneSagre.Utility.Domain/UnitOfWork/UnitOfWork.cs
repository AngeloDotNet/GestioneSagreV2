using GestioneSagre.Utility.Infrastructure.DataAccess;
using GestioneSagre.Utility.Infrastructure.Repository.Read;
using GestioneSagre.Utility.Infrastructure.Repository.Write;

namespace GestioneSagre.Utility.Domain.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataDbContext dbContext;
    public IUtilityReadRepository UtilityRead { get; }
    public IUtilityWriteRepository UtilityWrite { get; }

    public UnitOfWork(DataDbContext dbContext, IUtilityReadRepository utilityRead, IUtilityWriteRepository utilityWrite)
    {
        this.dbContext = dbContext;
        UtilityRead = utilityRead;
        UtilityWrite = utilityWrite;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            dbContext.Dispose();
        }
    }
}