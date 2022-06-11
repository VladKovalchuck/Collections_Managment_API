using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.Entity;

namespace CollectionsManagmentAPI.DataAccess.Repositories;

public class PgSqlItemRepository : IRepository<ItemEntity>
{
    public IEnumerable<ItemEntity> GetAll()
    {
        throw new NotImplementedException();
    }

    public ItemEntity GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Create(ItemEntity item)
    {
        throw new NotImplementedException();
    }

    public void Update(ItemEntity item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}