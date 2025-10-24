namespace ProductService.BusinessLayer.DTO;

public record UpdateProductRequest(Guid ProductId, string ProductName, string Category, double UnitPrice, int QuantityInStock)
{
    public UpdateProductRequest() : this(default, string.Empty, string.Empty, 0.0, 0)
    {
    }
}
