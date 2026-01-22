global using ClinicAppointment.BLL.Repositories;
global using System.Collections;
namespace ClinicAppointment.BLL;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private Hashtable _repositories;
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new();
    }
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var key = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(key))
        {
            var repository = new GenericRepository<TEntity>(_dbContext);
            _repositories.Add(key, repository);
        }
        return _repositories[key] as IGenericRepository<TEntity>;
    }
    public async Task<int> CompleteAsync()
       => await _dbContext.SaveChangesAsync();
    public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();
}