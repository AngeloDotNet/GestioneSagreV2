namespace GestioneSagre.Shared.GenericRepository.Read;

public interface IGenericReadRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
}