using AutoMapper;
using MB.Services.Catalog.Dtos;
using MB.Services.Catalog.Models;

namespace MB.Services.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<Feature,FeatureDto>().ReverseMap();

            CreateMap<Product,ProductCreateDto>().ReverseMap();
            CreateMap<Product,ProductUpdateDto>().ReverseMap();
        }
    }
}
