using API;
using AutoMapper;
using AutoMapper.Extensions.EnumMapping;

namespace Api.Mapping;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        CreateMap<Domain.Enums.ProductType, ProductType>()
            .ConvertUsingEnumMapping(opt =>
                opt.MapValue(Domain.Enums.ProductType.General, ProductType.General)
                    .MapValue(Domain.Enums.ProductType.HouseholdChemicals, ProductType.HouseholdChemicals)
                    .MapValue(Domain.Enums.ProductType.Appliances, ProductType.Appliances)
                    .MapValue(Domain.Enums.ProductType.Products, ProductType.Products))
            .ReverseMap();
    }
}