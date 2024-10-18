using Amazon.Core.Entities.Identity;
using Amazon.Services.ReviewServices.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ReviewServices
{
	public interface IReviewService
	{
		Task<IReadOnlyList<ReviewToReturnDto>> GetAllReviewsAsync();
		Task<ReviewToReturnDto> GetReviewByIdAsync(int id);
		Task<ReviewToReturnDto> AddReviewAsync(ReviewDto reviewDto,AppUser user);
		Task<ReviewToReturnDto> UpdateReviewAsync(ReviewDto reviewDto, AppUser user);
		Task<IReadOnlyList<ReviewToReturnDto>> DeleteReviewAsync(int id);
	}
}
