using Amazon.Services.ParentCategoryService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ParentCategoryService
{
    public interface IParentCategoryService
    {
        Task<IReadOnlyList<ParentCategoryToReturnDto>> GetAllParentCategoriesAsync();
        Task<ParentCategoryToReturnDto> AddParentCategory(ParentCategoryDto parentCategory);
        Task<IReadOnlyList<ParentCategoryToReturnDto>> DeleteParentCategory(int Id);
        Task<ParentCategoryToReturnDto> UpdateParentCategory(int Id, ParentCategoryDto parentCategory);
        Task<ParentCategoryToReturnDto> GetParentCategoryByIdAsync(int? Id);

    }
}
