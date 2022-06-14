using CollectionsManagmentAPI.DataAccess;
using CollectionsManagmentAPI.DataAccess.Interfaces;
using CollectionsManagmentAPI.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CollectionsManagmentAPI.Infrastructure;

public static class DataModule
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CollectionsDB");
        services.AddTransient(typeof(IRepository<>), typeof(PgSqlCollectionsRepository<>));
        services.AddDbContext<CollectionsDbContext>(options => { options.UseNpgsql(connectionString); });
        return services;
    }   
}