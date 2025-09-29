using Application.Dto;
using AutoMapper;
using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Models;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<long> AddAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);

        var productId = await _productRepository.AddAsync(product);

        return productId;
    }

    public async Task<Product> GetByIdAsync(long productId)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
            throw new ProductNotFoundException($"Product with id = {product} not found");

        return product;
    }

    public async Task<Product> UpdateCostByIdAsync(long productId, double newCost)
    {
        var product = await _productRepository.UpdateCostByIdAsync(productId, newCost);
        
        if (product is null)
            throw new ProductNotFoundException($"Product with id = {product} not found");

        return product;
    }

    public async Task<IEnumerable<Product>> GetProducts(ProductFilter filter)
    {
        var products = await _productRepository.GetProducts(filter);

        return products;
    }
}