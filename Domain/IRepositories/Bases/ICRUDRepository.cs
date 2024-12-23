namespace Domain.IRepositories.Bases;

public interface ICRUDRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(T entity);
    Task<Guid> UpdateAsync(T entity);
    Task DeleteByIdAsync(Guid id);
}
