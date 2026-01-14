namespace ProductService.BusinessLayer.Mapper;

public class UpdateProductRequestToProductMappingProfile : Profile
{
    public UpdateProductRequestToProductMappingProfile()
    {
        CreateMap<UpdateProductRequest, Product>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));
    }
}
