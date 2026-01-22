namespace ClinicAppointment.BLL.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
        => await _dbContext.Set<T>().FindAsync(id);

    public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        => await ApplySpecifications(spec).FirstOrDefaultAsync();

    public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        => await ApplySpecifications(spec).AsNoTracking().ToListAsync();

    public async Task<int> GetCountAsync(ISpecifications<T> spec)
        => await ApplySpecifications(spec).CountAsync();

    public void Add(T entity)
        => _dbContext.Set<T>().Add(entity);

    public void Update(T entity)
        => _dbContext.Set<T>().Update(entity);

    public void Delete(T entity)
        => _dbContext.Set<T>().Remove(entity);

    private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
        => SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
}