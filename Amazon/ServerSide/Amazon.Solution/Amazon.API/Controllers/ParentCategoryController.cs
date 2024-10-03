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
        public async Task<IReadOnlyList<ParentCategoryToReturnDto>> GetAllParentCategories()
             => await _parentCategoryService.GetAllParentCategoriesAsync();

        [HttpPost]
        public async Task<ParentCategoryToReturnDto> AddParentCategory(ParentCategoryDto parentCategory)
            => await _parentCategoryService.AddParentCategory(parentCategory);

        [HttpDelete("{Id}")]
        public async Task<IReadOnlyList<ParentCategoryToReturnDto>> DeleteParentCategory(int Id)
            =>await _parentCategoryService.DeleteParentCategory(Id);

        [HttpPut("id")]
        public async Task<ParentCategoryToReturnDto> UpdateParentCategory(int Id, ParentCategoryDto parentCategory)
            => await _parentCategoryService.UpdateParentCategory(Id, parentCategory);

        [HttpGet("{Id}")]
        public async Task<ParentCategoryToReturnDto> GetParentCategoryById(int? Id)
            => await _parentCategoryService.GetParentCategoryByIdAsync(Id);



    }
}
