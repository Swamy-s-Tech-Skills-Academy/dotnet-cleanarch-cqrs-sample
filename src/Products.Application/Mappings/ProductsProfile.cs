using AutoMapper;
using Products.Domain.Entities;
using Products.Shared.DTOs;

namespace Products.Application.Mappings;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category)); // Explicit mapping for Category
    }
}
