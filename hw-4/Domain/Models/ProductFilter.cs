using Domain.Enums;

namespace Domain.Models;

public class ProductFilter
{
    public DateTime? CreationDate { get; set; }
    public int? WarehouseId { get; set; }
    public ProductType? ProductType { get; set; }
}