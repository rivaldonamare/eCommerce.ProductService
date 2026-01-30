namespace ProductService.BusinessLayer.DTO;

public record UpdatedProductDTO
{
    public Guid Id { get; set; }
    public required string ProductName { get; set; }
}
