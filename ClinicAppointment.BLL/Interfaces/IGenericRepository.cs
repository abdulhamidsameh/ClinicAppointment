namespace ClinicAppointment.BLL.Interfaces;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> AnyWithSpecAsync(ISpecifications<T> spec);
    Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
    Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

    Task<int> GetCountAsync(ISpecifications<T> spec);
    void Add(T entity);
    void Update(T entity);

    void Delete(T entity);

}