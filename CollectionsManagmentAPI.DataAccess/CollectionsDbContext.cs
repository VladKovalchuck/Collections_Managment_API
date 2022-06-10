using Microsoft.EntityFrameworkCore;

namespace CollectionsManagmentAPI.DataAccess;

public class CollectionsDbContext : DbContext
{
    public CollectionsDbContext(DbContextOptions<CollectionsDbContext> options)
        : base(options)
    {
        
    }
    
}