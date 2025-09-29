using Domain.Models;

namespace Domain.Abstractions;

public interface IProductRepository
{
    Task<long> AddAsync(Product product);

    Task<Product?> GetByIdAsync(long productId);

    Task<Product?> UpdateCostByIdAsync(long productId, double newCost);

    Task<List<Product>> GetProducts(ProductFilter filter);
}