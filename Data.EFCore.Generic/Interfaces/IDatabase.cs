namespace Data.EFCore.Generic.Interfaces;

public interface IDatabase<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
}