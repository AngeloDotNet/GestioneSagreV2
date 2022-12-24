using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Shared.GenericRepository.Read;

public abstract class GenericReadRepository<T> : IGenericReadRepository<T> where T : class
{
    private readonly DbContext dbContext;

    protected GenericReadRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await dbContext.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await dbContext.Set<T>().FindAsync(id);

        if (entity == null)
        {
            return null;
        }

        dbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }
}