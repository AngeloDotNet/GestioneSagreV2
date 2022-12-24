using Microsoft.EntityFrameworkCore;

namespace GestioneSagre.Shared.GenericRepository.Write;

public abstract class GenericWriteRepository<T> : IGenericWriteRepository<T> where T : class
{
    private readonly DbContext dbContext;

    protected GenericWriteRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> CreateAsync(T entity)
    {
        dbContext.Set<T>().Add(entity);

        var result = await dbContext.SaveChangesAsync();

        dbContext.Entry(entity).State = EntityState.Detached;

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);

        var result = await dbContext.SaveChangesAsync();

        dbContext.Entry(entity).State = EntityState.Detached;

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);

        var result = await dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}