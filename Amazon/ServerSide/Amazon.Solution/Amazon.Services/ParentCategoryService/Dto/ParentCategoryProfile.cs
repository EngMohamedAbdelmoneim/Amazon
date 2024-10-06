using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.Entities;
using AutoMapper;

namespace Amazon.Services.ParentCategoryService.Dto
{
    public class ParentCategoryProfile : Profile
    {
        public ParentCategoryProfile()
        {
            CreateMap<ParentCategoryDto, ParentCategory>().ReverseMap();
            CreateMap<ParentCategory,ParentCategoryToReturnDto>()
                .ForMember(dest=>dest.Categories,options=>options.MapFrom(Src=>Src.Categories.Select(c => c.Name)));
        }
        
    }
}
