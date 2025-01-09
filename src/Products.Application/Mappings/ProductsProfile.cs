using AutoMapper;
using Products.Application.DTOs;
using Products.Domain.Entities;

namespace Products.Application.Mappings;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
    }
}
