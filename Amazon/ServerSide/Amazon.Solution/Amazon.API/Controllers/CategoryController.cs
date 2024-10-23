using Amazon.API.Errors;
using Amazon.Services.CategoryServices;
using Amazon.Services.CategoryServices.Dto;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IReadOnlyList<CategoryToReturnDto>>> GetAllCategories()
            => Ok(await _categoryService.GetAllCategoriesAsync());

        [HttpPost]
        public async Task<ActionResult<CategoryToReturnDto>> AddCategory(CategoryDto categoryDto)
        {
            var result = await _categoryService.AddCategory(categoryDto);
            if (result is null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<IReadOnlyList<CategoryToReturnDto>>> DeleteCategory(int Id)
        {

            var result =  await _categoryService.DeleteCategory(Id);
            if (result is null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }

        [HttpPut("id")]
        public async Task<ActionResult<CategoryToReturnDto>> UpdateCategory(int Id, CategoryDto categoryDto)
        {
            var result = await _categoryService.UpdateCategory(Id, categoryDto);
            if (result is null)
                return BadRequest(new ApiResponse(400));

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task <ActionResult<CategoryToReturnDto>> GetCategoryById(int? Id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(Id);
            if (result is null)
                return NotFound(new ApiResponse(404));
            return Ok(result);
        }
    }
}
