using Amazon.Services.BrandService.Dto;

namespace Amazon.Services.BrandService
{
    public interface IBrandService
    {
        Task<IReadOnlyList<BrandDto>> GetAllBrandsAsync();
        Task<BrandDto> GetBrandByIdAsync(int id);
        Task<BrandDto> AddBrand(BrandDto brand);
        Task<BrandDto> UpdateBrand(int id, BrandDto brand);
        Task<IReadOnlyList<BrandDto>> DeleteBrand(int id);
    }
}
