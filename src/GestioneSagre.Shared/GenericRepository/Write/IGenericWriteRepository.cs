namespace GestioneSagre.Shared.GenericRepository.Write;

public interface IGenericWriteRepository<T> where T : class
{
    Task<bool> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}