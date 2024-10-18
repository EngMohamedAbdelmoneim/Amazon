using Amazon.Services.ReviewServices.Dto;
using Amazon.Services.ReviewServices;
using Microsoft.AspNetCore.Mvc;
using Amazon.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Amazon.Core.Entities.Identity;

[Authorize]
public class ReviewController : BaseController
{
	private readonly IReviewService _reviewService;
	private readonly UserManager<AppUser> _userManager;

	public ReviewController(IReviewService reviewService,UserManager<AppUser> userManager)
	{
		_reviewService = reviewService;
		_userManager = userManager;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllReviews()
	{
		var reviews = await _reviewService.GetAllReviewsAsync();
		return Ok(reviews);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetReviewById(int id)
	{

		var review = await _reviewService.GetReviewByIdAsync(id);
		if (review == null)
		{
			return NotFound();
		}
		return Ok(review);
	}
	
	[HttpPost]
	public async Task<IActionResult> AddReview([FromBody] ReviewDto reviewDto)
	{
		var userEmail = User.FindFirstValue("Email"); 
		var user = await _userManager.FindByEmailAsync(userEmail);
		var returnValue = await _reviewService.AddReviewAsync(reviewDto, user);
		return Ok(returnValue);
	}
		
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDto reviewDto)
	{
		if (id != reviewDto.Id)
		{
			return BadRequest();
		}

		var userEmail = User.FindFirstValue("Email");
		var user = await _userManager.FindByEmailAsync(userEmail);
		await _reviewService.UpdateReviewAsync(reviewDto,user);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteReview(int id)
	{
		await _reviewService.DeleteReviewAsync(id);
		return NoContent();
	}
}
