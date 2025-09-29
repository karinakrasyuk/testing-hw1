using Domain.Enums;

namespace Domain.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Cost { get; set; }
    public double Weight { get; set; }
    public ProductType ProductType { get; set; }
    public DateTime CreationDate { get; set; }
    public long WarehouseId { get; set; }
}