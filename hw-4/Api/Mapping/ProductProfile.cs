using API;
using Application.Dto;
using AutoMapper;
using Product = Domain.Models.Product;

namespace Api.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<AddProductRequest, ProductDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ReverseMap();
        
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ReverseMap();
        
        CreateMap<Product, API.Product>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
            .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId))
            .ReverseMap();
    }
}