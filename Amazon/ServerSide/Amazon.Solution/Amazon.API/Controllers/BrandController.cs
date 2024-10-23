using Microsoft.AspNetCore.Mvc;
using Amazon.Services.BrandService;
using Amazon.Services.BrandService.Dto;
using Amazon.API.Errors;

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
        { 
            var result = await _brandService.AddBrand(product);
            if (result is null)
                return BadRequest(new ApiResponse(400));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BrandDto>> UpdateBrand(int id, BrandDto product)
        {
            var result = await _brandService.UpdateBrand(id, product);

			if (result is null)
				return BadRequest(new ApiResponse(400));
			return Ok(result);
		}


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands()
            => Ok( await _brandService.GetAllBrandsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> GetBrandById(int id)
        {
          var result =  await _brandService.GetBrandByIdAsync(id);

            if (result is null)
                return NotFound(new ApiResponse(404));
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> DeleteBrand(int id)
        {
            var result =  await _brandService.DeleteBrand(id);
            if (result is null)
                return NotFound(new ApiResponse(404));

            return Ok(result);

		}
    }
}
