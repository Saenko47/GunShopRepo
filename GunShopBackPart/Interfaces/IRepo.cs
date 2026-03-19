namespace GunShopBackPart.Interfaces
{
    public interface IRepo<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();

        Task AddAsync(T entity);
    }
}
