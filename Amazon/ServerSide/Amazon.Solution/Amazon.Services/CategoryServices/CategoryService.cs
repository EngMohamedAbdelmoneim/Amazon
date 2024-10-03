using Amazon.Services.CategoryServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            try
            {
                var mappedCategories = _mapper.Map<Category>(categoryDto);

                await _categoryRepo.Add(mappedCategories);

                var categoryToReturn = _mapper.Map<CategoryToReturnDto>(mappedCategories);

                return categoryToReturn;
            }
            catch(Exception ex)
            {
                throw new Exception($"Inner exception: {ex.InnerException?.Message}");
                throw new Exception("An unexpected error occured while adding parent category: " + ex.Message);
            }
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
               throw new Exception("An error occured while updating the parent category: Category doesn't exist");

            _mapper.Map(categoryDto, existingCategory);
            await _categoryRepo.Update(existingCategory);

            return _mapper.Map<CategoryToReturnDto>(existingCategory);
        }
        public async Task<CategoryToReturnDto> GetCategoryByIdAsync(int? Id)
        {
            var category = await _categoryRepo.GetByIdAsync(Id);

            var mappedCategory = _mapper.Map<CategoryToReturnDto>(category);

            return mappedCategory;
        }


    }
}
