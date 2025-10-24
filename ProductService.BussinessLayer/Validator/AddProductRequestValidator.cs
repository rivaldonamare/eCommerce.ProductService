namespace ProductService.BusinessLayer.Validator;

public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
{
    public AddProductRequestValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("Name is required").MaximumLength(100).WithMessage("Name must be less than 100 characters");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.UnitPrice).NotEmpty().WithMessage("Price is required");
        RuleFor(x => x.QuantityInStock).NotEmpty().WithMessage("Quantity is required");
    }
}
