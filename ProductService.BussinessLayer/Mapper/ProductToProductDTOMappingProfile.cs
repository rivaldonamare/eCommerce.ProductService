namespace ProductService.BusinessLayer.Mapper;

public class ProductToProductDTOMappingProfile : Profile
{
    public ProductToProductDTOMappingProfile()
    {
        CreateMap<string, ProductCategory>()
            .ConvertUsing(src =>
                string.IsNullOrWhiteSpace(src)
                    ? ProductCategory.Electronics
                    : (ProductCategory)Enum.Parse(typeof(ProductCategory), src, true)
            );

        CreateMap<Product, ProductDTO>()
           .ForMember(dest => dest.ProductID,
               opt => opt.MapFrom(src => src.ProductId))

           .ForMember(dest => dest.ProductName,
               opt => opt.MapFrom(src => src.ProductName))

           .ForMember(dest => dest.CategoryCode,
               opt => opt.MapFrom(src => src.Category))

           .ForMember(dest => dest.UnitPrice,
               opt => opt.MapFrom(src => src.UnitPrice))

           .ForMember(dest => dest.QuantityInStock,
               opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
