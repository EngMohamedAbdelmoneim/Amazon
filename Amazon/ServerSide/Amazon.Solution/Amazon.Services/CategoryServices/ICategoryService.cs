using Amazon.Services.CategoryServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryToReturnDto>> GetAllCategoriesAsync();

        Task<CategoryToReturnDto> AddCategory(CategoryDto categoryDto);
        Task<IReadOnlyList<CategoryToReturnDto>> DeleteCategory(int Id);
        Task<CategoryToReturnDto> UpdateCategory(int Id, CategoryDto categoryDto);
        Task <CategoryToReturnDto> GetCategoryByIdAsync(int? Id);
    }
}
