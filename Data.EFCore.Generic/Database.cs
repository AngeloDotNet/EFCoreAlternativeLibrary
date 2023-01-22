using Data.EFCore.Generic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.EFCore.Generic;

public class Database<T> : IDatabase<T> where T : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _set;

    public Database(DbContext dbContext)
    {
        _dbContext = dbContext;
        _set = _dbContext.Set<T>();
    }

    public Task<List<T>> GetAllAsync()
    {
        return _set.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _set.FindAsync(id);

        if (entity == null)
        {
            return null;
        }

        _dbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }
}