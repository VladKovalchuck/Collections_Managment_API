using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.DataAccess.Repositories;

public class PgSqlCollectionRepository : IRepository<CollectionEntity>
{
    private readonly CollectionsDbContext _context;
    public PgSqlCollectionRepository(CollectionsDbContext context) => _context = context;
    
    public IEnumerable<CollectionEntity> GetAll()
    {
        return _context.Collections.ToList();
    }

    public CollectionEntity GetById(int id)
    {
        return _context.Collections.Find(id);
    }

    public void Create(CollectionEntity item)
    {
        _context.Collections.Add(item);
        _context.SaveChanges();
    }

    public void Update(CollectionEntity item)
    {
        _context.Collections.Update(item);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        CollectionEntity collection = _context.Collections.Find(id);
        if (id != null)
            _context.Collections.Remove(collection);
    }
}