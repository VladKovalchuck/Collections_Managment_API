using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Service.Interfaces;
using CollectionsManagmentAPI.Service.Service;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace CollectionsManagmentAPI.Infrastructure;

public static class BusinessModule
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ICollectionService, CollectionService>();
        return services;
    }
}