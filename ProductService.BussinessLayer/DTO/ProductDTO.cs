namespace ProductService.BusinessLayer.DTO;

public record ProductDTO(Guid ProductId, string ProductName, ProductCategory Category, string CategoryCode, double UnitPrice, int QuantityInStock)
{
    public ProductDTO() : this(default, string.Empty, ProductCategory.Electronics, string.Empty, 0.0, 0)
    {
    }
}