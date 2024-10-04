using Amazon.Core.Entities;
using AutoMapper;

namespace Amazon.Services.BrandService.Dto
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
        }
    }
}
