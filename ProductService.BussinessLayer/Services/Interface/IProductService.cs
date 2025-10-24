namespace ProductService.BusinessLayer.Services.Interface;

public interface IProductService
{
    Task<List<ProductDTO>> GetProductsAsync();

    Task<List<ProductDTO>> GetProductsByConditionAsync(Expression<Func<Product, bool>> condition);

    Task<ProductDTO> GetSingleProductByConditionAsync(Expression<Func<Product, bool>> condition);

    Task<ProductDTO> AddProductAsync(AddProductRequest request);
    Task<ProductDTO> UpdateProductAsync(UpdateProductRequest request);
    Task<bool> DeleteProductAsync(Guid id);
}
