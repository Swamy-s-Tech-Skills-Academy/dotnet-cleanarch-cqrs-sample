using AutoMapper;
using Products.Application.DTOs;
using Products.Domain.Entities;

namespace Products.Application.Mappings;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category)); // Explicit mapping for Category

        CreateMap<Category, CategoryDto>();
    }
}
