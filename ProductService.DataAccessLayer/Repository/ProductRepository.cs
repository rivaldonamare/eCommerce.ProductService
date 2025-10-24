namespace ProductService.DataAccessLayer.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Product> AddProduct(Product product)
    {
        if (await _dbContext.Products.AnyAsync(x => x.ProductName.ToUpper() == product.ProductName.ToUpper()))
        {
            throw new InvalidOperationException("Product already exists");
        }

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.ProductId == id)
            ?? throw new InvalidOperationException("Product not found");

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
       return await _dbContext.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductWithCondition(Expression<Func<Product, bool>> expression)
    {
        return await _dbContext.Products.Where(expression).ToListAsync();
    }

    public async Task<Product> GetSingleProductWithCondition(Expression<Func<Product, bool>> expression)
    {
        var product = await _dbContext.Products.Where(expression).FirstOrDefaultAsync();
        return product ?? throw new InvalidOperationException("Product not found");
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var exisitingProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId)
            ?? throw new InvalidOperationException("Product not found");

        exisitingProduct.ProductName = product.ProductName;
        exisitingProduct.Category = product.Category;
        exisitingProduct.UnitPrice = product.UnitPrice;
        exisitingProduct.QuantityInStock = product.QuantityInStock;

        await _dbContext.SaveChangesAsync();
        return product;
    }
}
