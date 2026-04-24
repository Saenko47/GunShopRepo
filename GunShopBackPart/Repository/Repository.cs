using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Mappers;
using GunShopBackPart.Models;
using GunShopBackPart.Tool;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GunShopBackPart.Repository
{
    public class Repository<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<T> set;

        public Repository(ApplicationDBContext context)
        {
            _context = context;
            set = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) 
        { 
        return await set.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await set.ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(int id)
        {
            var entity = await set.FindAsync(id);
            if (entity != null)
            {
                set.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }



    }
}
