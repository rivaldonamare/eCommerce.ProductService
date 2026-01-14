namespace ProductService.API.APIEndpoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        #region GET Methods
        // GET /api/products - Get all products
        app.MapGet("/api/products", async (IProductService productService) =>
        {
            var products = await productService.GetProductsAsync();
            return Results.Ok(products);
        })
        .WithName("GetAllProducts")
        .WithSummary("Get all products")
        .WithDescription("Retrieves a list of all products available in the system.")
        .Produces<List<ProductDTO>>(StatusCodes.Status200OK);

        // GET /api/products/{id} - Get a single product by ID
        app.MapGet("/api/products/search/product-id/{id}", async (Guid id, IProductService productService) =>
        {
            var product = await productService.GetSingleProductByConditionAsync(p => p.ProductId == id);
            return Results.Ok(product);
        })
        .WithName("GetProductById")
        .WithSummary("Get a single product by ID")
        .WithDescription("Retrieves a single product by its ID.")
        .Produces<ProductDTO>(StatusCodes.Status200OK);

        // GET /api/products/{filterType}/{value} - Get products by dynamic filter
        app.MapGet("/api/products/{filterType}/{value}", async (string filterType, string value, IProductService productService) =>
        {
            var products = filterType.ToLower() switch
            {
                "name" => await productService.GetProductsByConditionAsync(p => p.ProductName.Contains(value)),
                "category" => await productService.GetProductsByConditionAsync(p => p.Category == value),
                _ => Enumerable.Empty<ProductDTO>()
            };

            return products.Any()
                ? Results.Ok(products)
                : Results.NotFound($"No products found for {filterType} = {value}");
        })
        .WithName("GetProductsByDynamicFilter")
        .WithSummary("Get products by dynamic filter")
        .WithDescription("Retrieves products by filter type (name, id, category, etc.)")
        .Produces<ProductDTO[]>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        #endregion

        #region POST/PUT/DELETE Methods
        // POST /api/products - Add a new product
        app.MapPost("/api/products", async (AddProductRequest request, IValidator<AddProductRequest> AddProductRequestValidator, IProductService productService) =>
        {
            await AddProductRequestValidator.ValidateAsync(request);
            var product = await productService.AddProductAsync(request);
            return Results.Created($"/api/products/{product.ProductID}", product);
        })
        .WithName("AddProduct")
        .WithSummary("Add a new product")
        .WithDescription("Adds a new product to the system.")
        .Accepts<AddProductRequest>("application/json")
        .Produces<ProductDTO>(StatusCodes.Status201Created);

        // PUT /api/products/{id} - Update an existing product
        app.MapPut("/api/products", async (UpdateProductRequest request, IValidator<UpdateProductRequest> UpdateProductRequestValidator, IProductService productService) =>
        {
            await UpdateProductRequestValidator.ValidateAndThrowAsync(request);
            var product = await productService.UpdateProductAsync(request);
            return Results.Ok(product);
        })
        .WithName("UpdateProduct")
        .WithSummary("Update an existing product")
        .WithDescription("Updates an existing product in the system.")
        .Accepts<UpdateProductRequest>("application/json")
        .Produces<ProductDTO>(StatusCodes.Status200OK);

        // DELETE /api/products/{id} - Delete a product
        app.MapDelete("/api/products/{id}", async (Guid id, IProductService productService) =>
        {
            await productService.DeleteProductAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteProduct")
        .WithSummary("Delete a product")
        .WithDescription("Deletes a product from the system.")
        .Produces(StatusCodes.Status204NoContent);

        #endregion

        return app;
    }
}
