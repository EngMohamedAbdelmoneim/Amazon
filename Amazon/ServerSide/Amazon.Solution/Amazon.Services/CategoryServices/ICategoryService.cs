using Amazon.Services.CategoryServices.Dto;

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
