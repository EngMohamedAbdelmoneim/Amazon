using Microsoft.AspNetCore.Mvc;
using Amazon.Services.BrandService;
using Amazon.Services.BrandService.Dto;

namespace Amazon.API.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }


        [HttpPost]
        [ActionName("AddBrand")]
        public async Task<ActionResult<BrandDto>> AddBrand(BrandDto product)
            => await _brandService.AddBrand(product);


        [HttpPut("id")]
        public async Task<ActionResult<BrandDto>> UpdateBrand(int id, BrandDto product)
            => await _brandService.UpdateBrand(id, product);


        [HttpGet]
        public async Task<IReadOnlyList<BrandDto>> GetAllBrands()
            => await _brandService.GetAllBrandsAsync();

        [HttpGet("{id}")]
        public async Task<BrandDto> GetBrandById(int id)
          => await _brandService.GetBrandByIdAsync(id);


        [HttpDelete("{id}")]
        public async Task<IReadOnlyList<BrandDto>> DeleteBrand(int id)
           => await _brandService.DeleteBrand(id);
    }
}
