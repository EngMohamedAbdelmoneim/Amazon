using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.Entities;
using Amazon.Services.ParentCategoryService.Dto;
using Amazone.Infrastructure.Interfaces;
using AutoMapper;
namespace Amazon.Services.ParentCategoryService
{
    public class ParentCategoryService : IParentCategoryService 
    {
        private readonly IGenericRepository <ParentCategory> _parentCategoryRepo;
        private readonly IMapper _mapper;
        public ParentCategoryService(IGenericRepository<ParentCategory> parentCategoryRepo , IMapper mapper)
        {
            _parentCategoryRepo = parentCategoryRepo;
            _mapper = mapper;
        }

        public async Task <IReadOnlyList<ParentCategoryToReturnDto>> GetAllParentCategoriesAsync()
        {
            var parentCategory = await _parentCategoryRepo.GetAllAsync();
            var mappedParentCategory = _mapper.Map<IReadOnlyList<ParentCategoryToReturnDto>>(parentCategory);
            return mappedParentCategory;
        }

        public async Task <ParentCategoryToReturnDto> AddParentCategory(ParentCategoryDto parentCategoryDto)
        {

            var mappedParentCategory = _mapper.Map<ParentCategory>(parentCategoryDto);
            if (mappedParentCategory is null)
                return null;

            await _parentCategoryRepo.Add(mappedParentCategory);

            var parentCategoryToReturn = _mapper.Map<ParentCategoryToReturnDto>(mappedParentCategory);

            return parentCategoryToReturn;
        }

        public async Task <IReadOnlyList<ParentCategoryToReturnDto>> DeleteParentCategory(int Id)
        {
            var parentCategory = await _parentCategoryRepo.GetByIdAsync(Id);

            if (parentCategory is null)
                return null;

            await _parentCategoryRepo.Delete(parentCategory);

            var parentCategories = await GetAllParentCategoriesAsync();

            return parentCategories;
            
        }

        public async Task <ParentCategoryToReturnDto> UpdateParentCategory(int Id , ParentCategoryDto parentCategoryDto)
        {
            var existingParentCategory = await _parentCategoryRepo.GetByIdAsync(Id);

            if (existingParentCategory is null)
                return null;
            _mapper.Map(parentCategoryDto, existingParentCategory);

            await _parentCategoryRepo.Update(existingParentCategory);

            return _mapper.Map<ParentCategoryToReturnDto>(existingParentCategory);
        }

        public async Task <ParentCategoryToReturnDto> GetParentCategoryByIdAsync(int? id)
        {
            var parenCategory = await _parentCategoryRepo.GetByIdAsync(id);
            if (parenCategory is null)
                return null;
            var mappedParentCategory = _mapper.Map<ParentCategoryToReturnDto>(parenCategory);

            return mappedParentCategory;
        }
    }
}
