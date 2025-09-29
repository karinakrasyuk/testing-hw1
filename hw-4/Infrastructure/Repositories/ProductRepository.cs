using System.Collections.Concurrent;
using Domain.Abstractions;
using Domain.Models;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<long, Product> _store = new();
    private volatile int _amountAdded = 0;
    private readonly object _lock = new();

    public Task<long> AddAsync(Product product)
    {
        long productId;

        lock (_lock)
        {
            productId = _amountAdded++;
            product.Id = productId;
        }

        _store.TryAdd(productId, product);
        
        return Task.FromResult(productId);
    }

    public Task<Product?> GetByIdAsync(long productId)
    {
        if (!_store.TryGetValue(productId, out var product))
            return Task.FromResult((Product?)null);

        return Task.FromResult(product);
    }

    public Task<Product?> UpdateCostByIdAsync(long productId, double newCost)
    {
        if (!_store.TryGetValue(productId, out var product))
            return Task.FromResult((Product?)null);

        lock (_lock)
        {
            product.Cost = newCost;
        }

        return Task.FromResult(product);
    }

    public Task<List<Product>> GetProducts(ProductFilter filter)
    {
        var products = _store.Values.AsEnumerable();

        if (filter.CreationDate is not null)
            products = products.Where(x => x.CreationDate == filter.CreationDate);

        if (filter.WarehouseId is not null)
            products = products.Where(x => x.WarehouseId == filter.WarehouseId);

        if (filter.ProductType is not null)
            products = products.Where(x => x.ProductType == filter.ProductType);

        var result = products.ToList();

        return Task.FromResult(result);
    }
}
