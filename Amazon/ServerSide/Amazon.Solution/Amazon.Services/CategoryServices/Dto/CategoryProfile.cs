using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.Entities;
using AutoMapper;

namespace Amazon.Services.CategoryServices.Dto
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<Category, CategoryToReturnDto>()
                .ForMember(dest => dest.ParentCategoryName, options => options.MapFrom(src => src.ParentCategory.Name));
        }
    }
}
