using Application.Dto;
using Application.Services;
using AutoBogus;
using AutoMapper;
using Domain.Abstractions;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models;
using FluentAssertions;
using Moq;

namespace UnitTests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryFake = new(MockBehavior.Strict);
    private readonly Mock<IMapper> _mapperFake = new(MockBehavior.Strict);

    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        _productService = new ProductService(_productRepositoryFake.Object, _mapperFake.Object);
    }

    [Fact]
    public async Task GetById_ProductExistInRepository_ShouldReturnsProduct()
    {
        var expectedProduct = new AutoFaker<Product>().Generate();
        var productId = expectedProduct.Id;

        _productRepositoryFake
            .Setup(f => f.GetByIdAsync(productId))
            .ReturnsAsync(expectedProduct);

        Product actualProduct = await _productService.GetByIdAsync(productId);

        actualProduct.Should().BeEquivalentTo(expectedProduct);
    }

    [Fact]
    public async Task GetById_ProductNotExistInRepository_RepositoryException()
    {
        var expectedProduct = new AutoFaker<Product>().Generate();
        expectedProduct.Id = 1;
        var productId = 0;

        _productRepositoryFake
            .Setup(f => f.GetByIdAsync(productId))
            .ReturnsAsync((Product?)null);

        var actualProduct = async () => await _productService.GetByIdAsync(productId);

        await actualProduct.Should().ThrowAsync<ProductNotFoundException>();
    }

    [Fact]
    public async Task UpdateCost_ProductExistInRepository_ShouldReturnProductWithNewCost()
    {
        var expectedProduct = new AutoFaker<Product>().Generate();
        var productId = expectedProduct.Id;
        var cost = expectedProduct.Cost;

        _productRepositoryFake
            .Setup(f => f.UpdateCostByIdAsync(productId, cost))
            .ReturnsAsync(expectedProduct);

        var actualProduct = await _productService.UpdateCostByIdAsync(productId, cost);

        actualProduct.Should().BeEquivalentTo(expectedProduct);
    }

    [Fact]
    public async Task UpdateCost_ProductNotExistInRepository_RepositoryException()
    {
        var expectedProduct = new AutoFaker<Product>().Generate();
        expectedProduct.Id = 1;
        var productId = 0;
        var cost = expectedProduct.Cost;

        _productRepositoryFake
            .Setup(f => f.UpdateCostByIdAsync(productId, cost))
            .ReturnsAsync((Product?)null);

        var actualProduct = async () => await _productService.UpdateCostByIdAsync(productId, cost);

        await actualProduct.Should().ThrowAsync<ProductNotFoundException>();
    }

    [Fact]
    public async Task GetProducts_ValidFilter_ShouldReturnProductsBasedOnFilter()
    {
        var filter = new ProductFilter { CreationDate = DateTime.Now, ProductType = ProductType.Appliances };
        var expectedProducts = new List<Product>
        {
            new()
                { Id = 1, Name = "Product1", Cost = 10.0 },
            new()
                { Id = 2, Name = "Product2", Cost = 15.0 }
        };
        _productRepositoryFake
            .Setup(r =>
                r.GetProducts(It.Is<ProductFilter>(x
                    => x.CreationDate == filter.CreationDate &&
                       x.ProductType == filter.ProductType &&
                       x.WarehouseId == filter.WarehouseId)))
            .ReturnsAsync(expectedProducts);

        var result = await _productService.GetProducts(filter);

        result.Should().BeEquivalentTo(expectedProducts);
    }

    [Fact]
    public async Task GetProducts_EmptyResult_ShouldReturnEmptyList()
    {
        var filter = new ProductFilter { WarehouseId = 1 };
        var products = new List<Product>();
        _productRepositoryFake.Setup(r => r.GetProducts(filter)).ReturnsAsync(products);

        var result = await _productService.GetProducts(filter);

        result.Should().BeEquivalentTo(products);
    }

    [Fact]
    public async Task Add_ValidProduct_ShouldReturnProductId()
    {
        var productDto = new AutoFaker<ProductDto>().Generate();
        var product = new AutoFaker<Product>().Generate();
        var expectedProductId = 1;
        _mapperFake.Setup(f => f.Map<Product>(productDto)).Returns(product);
        _productRepositoryFake.Setup(f => f.AddAsync(product)).ReturnsAsync(expectedProductId);

        var actualProductId = await _productService.AddAsync(productDto);

        actualProductId.Should().Be(expectedProductId);
    }
}
