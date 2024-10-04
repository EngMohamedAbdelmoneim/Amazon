using Amazon.Core.Entities;
using Amazon.Services.BrandService.Dto;
using Amazone.Infrastructure.Interfaces;
using AutoMapper;

namespace Amazon.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly IGenericRepository<Brand> _brandRepo;
        private readonly IMapper _mapper;

        public BrandService(IGenericRepository<Brand> brandRepo, IMapper mapper)
        {
            _brandRepo = brandRepo;
            _mapper = mapper;
        }

        public async Task<BrandDto> AddBrand(BrandDto brand)
        {
            try
            {
                var mappedBrand = _mapper.Map<Brand>(brand);

                await _brandRepo.Add(mappedBrand);

                var brandToReturn = _mapper.Map<BrandDto>(mappedBrand);
                return brandToReturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IReadOnlyList<BrandDto>> DeleteBrand(int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            if (brand is null)
                return null;

            await _brandRepo.Delete(brand);

            var brands = await GetAllBrandsAsync();
            return brands;
        }

        public async Task<IReadOnlyList<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _brandRepo.GetAllAsync();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return mappedBrands;
        }

        public async Task<BrandDto> GetBrandByIdAsync(int id)
        {
            var brand = await _brandRepo.GetByIdAsync(id);
            var mappedBrand = _mapper.Map<BrandDto>(brand);
            return mappedBrand;
        }

        public async Task<BrandDto> UpdateBrand(int id, BrandDto brand)
        {
            var existintBrand = await _brandRepo.GetByIdAsync(id);
            if (existintBrand == null)
                throw new Exception("Brand Doesn't Exist");

            existintBrand.Name = brand.Name;

            await _brandRepo.Update(existintBrand);
            return _mapper.Map<BrandDto>(existintBrand);
        }
    }
}
