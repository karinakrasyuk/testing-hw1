using Domain.Enums;

namespace Application.Dto;

public record ProductDto(
    string Name,
    double Cost,
    double Weight,
    ProductType ProductType,
    DateTime CreationDate,
    long WarehouseId);