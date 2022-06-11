using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManagmentAPI.DataAccess.Repositories;

public class PgSqlCollectionsRepository<T> : IRepository<T> where T : class
{
    private readonly CollectionsDbContext _context;
    private DbSet<T> dbSet;

    public PgSqlCollectionsRepository(CollectionsDbContext context)
    {
        _context = context;
        dbSet = _context.Set<T>();
    } 
    
    public async Task<IQueryable<T>> GetAll()
    {
        return await dbSet.AsNoTracking().ToListAsync() as IQueryable<T>;
    }

    public async Task<T> GetById(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task Create(T item)
    {
        await dbSet.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T item)   
    {
        dbSet.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id)
    {
        T collection = await dbSet.FindAsync(id);
        if (id != null)
        {
            dbSet.Remove(collection);
            return true;
        }

        return false;
    }
}