using API;
using AutoMapper;

namespace Api.Mapping;

public class FilterProfile : Profile
{
    public FilterProfile()
    {
        CreateMap<Filter, Domain.Models.ProductFilter>()
            .ForMember(dest => dest.CreationDate, opt => opt.Condition(src => src?.CreationDate is not null))
            .ForMember(dest => dest.ProductType, opt => opt.Condition(src => src?.ProductType is not null))
            .ForMember(dest => dest.WarehouseId, opt => opt.Condition(src => src?.WarehouseId is not null))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.ProductType))
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId.Value));
    }
}