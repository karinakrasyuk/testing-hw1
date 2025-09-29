using API;
using Application.Dto;
using Application.Services;
using AutoMapper;
using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Product = API.Product;
using ProductService = API.ProductService;

namespace Api.GrpcServices;

public class ProductGrpcService : ProductService.ProductServiceBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductGrpcService(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public override async Task<AddProductResponse> Add(AddProductRequest request, ServerCallContext context)
    {
        var productDto = _mapper.Map<ProductDto>(request);

        var productId = await _productService.AddAsync(productDto);

        return new AddProductResponse
        {
            Id = productId
        };
    }

    public override async Task<Product> GetById(GetProductRequest request, ServerCallContext context)
    {
        var product = await _productService.GetByIdAsync(request.Id);

        return _mapper.Map<Product>(product);
    }

    public override async Task<UpdateCostResponse> UpdateCostById(UpdateCostRequest request, ServerCallContext context)
    {
        var product = await _productService.UpdateCostByIdAsync(request.Id, request.NewCost);

        return new UpdateCostResponse
        {
            Product = _mapper.Map<Product>(product)
        };
    }

    public override async Task<ProductsResponse> GetProducts(GetProductsRequest request, ServerCallContext context)
    {
        var filter = _mapper.Map<ProductFilter>(request.Filters);

        var products = await _productService.GetProducts(filter);

        var result = products.Select(x => _mapper.Map<Product>(x));

        var response = new ProductsResponse();
        response.Product.AddRange(result);

        return response;
    }
}
