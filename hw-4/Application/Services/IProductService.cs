using Application.Dto;
using Domain.Models;

namespace Application.Services;

public interface IProductService
{
    Task<long> AddAsync(ProductDto productDto);

    Task<Product> GetByIdAsync(long productId);

    Task<Product> UpdateCostByIdAsync(long productId, double newCost);
    Task<IEnumerable<Product>> GetProducts(ProductFilter filter);
}
