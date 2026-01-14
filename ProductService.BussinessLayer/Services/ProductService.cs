namespace ProductService.BusinessLayer.Services;

public class ProductService : IProductService
{
    private readonly IValidator<AddProductRequest> _addProductRequestValidator;
    private readonly IValidator<UpdateProductRequest> _updateProductRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IValidator<AddProductRequest> addProductRequestValidator, IValidator<UpdateProductRequest> updateProductRequestValidator, 
        IMapper mapper, IProductRepository productRepository)
    {
        _addProductRequestValidator = addProductRequestValidator ?? throw new ArgumentNullException(nameof(addProductRequestValidator));
        _updateProductRequestValidator = updateProductRequestValidator ?? throw new ArgumentNullException(nameof(updateProductRequestValidator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ProductDTO> AddProductAsync(AddProductRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        // validate request
        var validationResult = await _addProductRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = await _productRepository.AddProduct(_mapper.Map<Product>(request));
        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }

        // check if product exists
        await _productRepository.GetSingleProductWithCondition(x => x.ProductId == id);

        return await _productRepository.DeleteProduct(id);
    }

    public async Task<List<ProductDTO>> GetProductsAsync()
    {
        var products = await _productRepository.GetAllProducts();
        return products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
    }

    public async Task<List<ProductDTO>> GetProductsByConditionAsync(Expression<Func<Product, bool>> condition)
    {
        var products = await _productRepository.GetProductWithCondition(condition);
        return products.Select(x => _mapper.Map<ProductDTO>(x)).ToList();
    }

    public async Task<ProductDTO> GetSingleProductByConditionAsync(Expression<Func<Product, bool>> condition)
    {
        var product = await _productRepository.GetSingleProductWithCondition(condition);    
        return _mapper.Map<ProductDTO>(product); 
    }

    public async Task<ProductDTO?> UpdateProductAsync(UpdateProductRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        // check if product exists
        await _productRepository.GetSingleProductWithCondition(x => x.ProductId == request.ProductID);

        // validate request
        var validationResult = _updateProductRequestValidator.Validate(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await _productRepository.UpdateProduct(_mapper.Map<Product>(request));
        return _mapper.Map<ProductDTO>(request);
    }
}
