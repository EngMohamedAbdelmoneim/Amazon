using Amazon.Core.Entities;
using Amazon.Services.ReviewServices.Dto;
using Amazon.Services.ReviewServices;
using Amazone.Infrastructure.Interfaces;
using AutoMapper;
using Amazon.Core.Entities.Identity;

public class ReviewService : IReviewService
{
	private readonly IGenericRepository<Review> _reviewRepo;
	private readonly IMapper _mapper;

	public ReviewService(IGenericRepository<Review> reviewRepo, IMapper mapper)
	{
		_reviewRepo = reviewRepo;
		_mapper = mapper;
	}

	public async Task<IReadOnlyList<ReviewToReturnDto>> GetAllReviewsAsync()
	{
		var reviews = await _reviewRepo.GetAllAsync();
		var mappedReviews = _mapper.Map<IReadOnlyList<ReviewToReturnDto>>(reviews);
		return mappedReviews;
	}

	public async Task<ReviewToReturnDto> GetReviewByIdAsync(int id)
	{
		var review = await _reviewRepo.GetByIdAsync(id);
		if (review is null)
		{
			return null;
		}
		var mappedReview = _mapper.Map<ReviewToReturnDto>(review);
		return mappedReview;
	}

	public async Task<ReviewToReturnDto> AddReviewAsync(ReviewDto reviewDto,AppUser user)
	{
			reviewDto.AppUserEmail = user.Email;
			reviewDto.AppUserName = user.DisplayName;
			var review = _mapper.Map<Review>(reviewDto);
			await _reviewRepo.Add(review);
			var mappedReview = _mapper.Map<ReviewToReturnDto>(review);
			return mappedReview;
	
	}

 	public async Task<ReviewToReturnDto> UpdateReviewAsync(ReviewDto reviewDto, AppUser user)
	{
		reviewDto.AppUserEmail = user.Email;
		reviewDto.AppUserName = user.DisplayName;
		var review = await _reviewRepo.GetByIdAsync(reviewDto.Id);
		if (review is null)
		{
				throw new Exception("Review not found");
		}

		_mapper.Map(reviewDto, review);
		await _reviewRepo.Update(review);
		var mappedReview = _mapper.Map<ReviewToReturnDto>(review);
		return mappedReview; 
	}

	public async Task<IReadOnlyList<ReviewToReturnDto>> DeleteReviewAsync(int id)
	{
		var review = await _reviewRepo.GetByIdAsync(id);
		if (review is null)
		{
			throw new Exception("Review not found");
		}
		await _reviewRepo.Delete(review);
		var reviews = await GetAllReviewsAsync();
		return reviews;
	}
}
