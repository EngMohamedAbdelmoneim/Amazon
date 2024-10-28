using Amazon.Core.Entities.Identity;
using Amazon.Services.ProductService.Dto;

namespace AdminWebApplication.Models
{
    public class SellerDetailsViewModel
    {
        public AppUser Seller { get; set; }
        public IEnumerable<ProductToReturnDto> Products { get; set; }
    }
}
