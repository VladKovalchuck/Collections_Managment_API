using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace CollectionsManagmentAPI.DataAccess.Repositories;

public class PgSqlCollectionsRepository<T> : IRepository<T> where T : class
{
    private readonly CollectionsDbContext _context;
    public PgSqlCollectionsRepository(CollectionsDbContext context) => _context = context;
    
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async void Create(T item)
    {
        await _context.Set<T>().AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async void Update(T item)   
    {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync();
    }

    public async void Delete(int id)
    {
        T collection = await _context.Set<T>().FindAsync(id);
        if (id != null)
            _context.Set<T>().Remove(collection);
    }
}