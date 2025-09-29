using Domain.Abstractions;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IProductRepository, ProductRepository>();
    }
}