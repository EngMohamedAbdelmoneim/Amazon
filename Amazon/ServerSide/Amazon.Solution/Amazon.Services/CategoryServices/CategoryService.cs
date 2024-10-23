using Amazon.Services.CategoryServices.Dto;
using AutoMapper;
using Amazone.Infrastructure.Interfaces;
using Amazon.Core.Entities;

namespace Amazon.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<CategoryToReturnDto>> GetAllCategoriesAsync()
        {
            var category = await _categoryRepo.GetAllAsync();
            var mappedCategories = _mapper.Map<IReadOnlyList<CategoryToReturnDto>>(category);
            return mappedCategories;
        }
        public async Task<CategoryToReturnDto> AddCategory(CategoryDto categoryDto)
        {

            var mappedCategories = _mapper.Map<Category>(categoryDto);

            if (mappedCategories is null)
                return null;
            await _categoryRepo.Add(mappedCategories);

            var categoryToReturn = _mapper.Map<CategoryToReturnDto>(mappedCategories);

            return categoryToReturn;
            
        }

        public async Task<IReadOnlyList<CategoryToReturnDto>> DeleteCategory(int Id)
        {
            var category = await _categoryRepo.GetByIdAsync(Id);

            if (category is null)
                return null;
            await _categoryRepo.Delete(category);

            var mappedCategories = await GetAllCategoriesAsync();

            return mappedCategories;
        }
        public async Task<CategoryToReturnDto> UpdateCategory(int Id, CategoryDto categoryDto)
        {
            var existingCategory = await _categoryRepo.GetByIdAsync(Id);

            if (existingCategory is null)
                return null;
            _mapper.Map(categoryDto, existingCategory);
            await _categoryRepo.Update(existingCategory);

            return _mapper.Map<CategoryToReturnDto>(existingCategory);
        }
        public async Task<CategoryToReturnDto> GetCategoryByIdAsync(int? Id)
        {
            var category = await _categoryRepo.GetByIdAsync(Id);

            if (category is null)
                return null;

            var mappedCategory = _mapper.Map<CategoryToReturnDto>(category);

            return mappedCategory;
        }


    }
}
