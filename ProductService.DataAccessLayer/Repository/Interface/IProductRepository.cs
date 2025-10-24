namespace ProductService.DataAccessLayer.Repository.Interface;

public interface IProductRepository 
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<IEnumerable<Product>> GetProductWithCondition(Expression<Func<Product, bool>> expression);
    Task<Product> GetSingleProductWithCondition(Expression<Func<Product, bool>> expression);
    Task<Product> AddProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Guid id);
}
