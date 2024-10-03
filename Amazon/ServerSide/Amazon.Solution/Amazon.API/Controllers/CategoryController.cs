using Amazon.Services.CategoryServices;
using Amazon.Services.CategoryServices.Dto;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Amazon.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IReadOnlyList<CategoryToReturnDto>> GetAllCategories()
            => await _categoryService.GetAllCategoriesAsync();

        [HttpPost]
        public async Task<CategoryToReturnDto> AddCategory(CategoryDto categoryDto)
            => await _categoryService.AddCategory(categoryDto);

        [HttpDelete]
        public async Task<IReadOnlyList<CategoryToReturnDto>> DeleteCategory(int Id)
            => await _categoryService.DeleteCategory(Id);

        [HttpPut("id")]
        public async Task<CategoryToReturnDto> UpdateCategory(int Id, CategoryDto categoryDto)
            => await _categoryService.UpdateCategory(Id, categoryDto);

        [HttpGet("{Id}")]
        public async Task <CategoryToReturnDto> GetCategoryById(int? Id)
            =>await _categoryService.GetCategoryByIdAsync(Id);
    }
}
