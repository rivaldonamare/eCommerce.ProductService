namespace ProductService.BusinessLayer.DTO;

public record AddProductRequest(string ProductName, string Category, double UnitPrice, int QuantityInStock)
{
    public AddProductRequest() : this(string.Empty, string.Empty, 0.0, 0)
    {
    }
}
