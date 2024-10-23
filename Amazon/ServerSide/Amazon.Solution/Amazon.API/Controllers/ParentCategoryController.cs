using Amazon.API.Errors;
using Amazon.Services.ParentCategoryService;
using Amazon.Services.ParentCategoryService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API.Controllers
{
    public class ParentCategoryController : BaseController
    {
        private readonly IParentCategoryService _parentCategoryService;

        public ParentCategoryController(IParentCategoryService parentCategoryService)
        {
            _parentCategoryService = parentCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ParentCategoryToReturnDto>>> GetAllParentCategories()
             => Ok(await _parentCategoryService.GetAllParentCategoriesAsync());

        [HttpPost]
        public async Task<ActionResult<ParentCategoryToReturnDto>> AddParentCategory(ParentCategoryDto parentCategory)
        {
            var result = await _parentCategoryService.AddParentCategory(parentCategory);
            if (result is null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<IReadOnlyList<ParentCategoryToReturnDto>>> DeleteParentCategory(int Id)
        {
            var result = await _parentCategoryService.DeleteParentCategory(Id);
            if (result is null)
                return NotFound(new ApiResponse(400));
            return Ok(result);
        }

        [HttpPut("id")]
        public async Task<ActionResult<ParentCategoryToReturnDto>> UpdateParentCategory(int Id, ParentCategoryDto parentCategory)
        {
            var result = await _parentCategoryService.UpdateParentCategory(Id, parentCategory);
            if (result is null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ParentCategoryToReturnDto>> GetParentCategoryById(int? Id)
        {
            var result = await _parentCategoryService.GetParentCategoryByIdAsync(Id);
            if (result is null)
                return NotFound(new ApiResponse(404));
            return Ok(result);
        }
    }
}
