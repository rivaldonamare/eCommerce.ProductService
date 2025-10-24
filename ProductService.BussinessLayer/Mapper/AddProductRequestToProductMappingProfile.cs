namespace ProductService.BusinessLayer.Mapper;

public class AddProductRequestToProductMappingProfile : Profile
{
    public AddProductRequestToProductMappingProfile()
    {
        CreateMap<AddProductRequest, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));

        CreateMap<Product, ProductDTO>()
           .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
           .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
           .ForMember(dest => dest.CategoryCode, opt => opt.MapFrom(src => src.Category.ToString()))
           .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
           .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));

    }
}
