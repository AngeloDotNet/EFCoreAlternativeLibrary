using Data.EFCore.Generic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.EFCore.Generic;

public class Command<T> : ICommand<T> where T : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _set;

    public Command(DbContext dbContext)
    {
        _dbContext = dbContext;
        _set = _dbContext.Set<T>();
    }

    public async Task CreateAsync(T entity)
    {
        _set.Add(entity);

        await _dbContext.SaveChangesAsync();

        _dbContext.Entry(entity).State = EntityState.Detached;
    }

    public async Task UpdateAsync(T entity)
    {
        _set.Update(entity);

        await _dbContext.SaveChangesAsync();

        _dbContext.Entry(entity).State = EntityState.Detached;
    }

    public async Task DeleteAsync(T entity)
    {
        _set.Remove(entity);

        await _dbContext.SaveChangesAsync();

        _dbContext.Entry(entity).State = EntityState.Detached;
    }
}