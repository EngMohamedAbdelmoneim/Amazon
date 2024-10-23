using Amazon.API.Errors;
using Amazon.Core.DBContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amazon.API.Controllers
{

	public class BuggyController : BaseController
	{
		private readonly AmazonDbContext _context;

		public BuggyController(AmazonDbContext context)
        {
			_context = context;
		}
		[HttpGet("NotFound")]
		public ActionResult GetNotFoundRequest()
		{
			var Product = _context.Products.Find(100);
			if (Product == null)
				return NotFound(new ApiResponse(404));
			//if (Product is null) return NotFound(new ApiResponse(404));
			return Ok(Product);
		}
		[HttpGet("ServerError")]
		public ActionResult GetServerError()
		{
			var Product = _context.Products.Find(1000);
			var ProductToResult = Product.ToString();
			return Ok(ProductToResult);
		}
		[HttpGet("BadRequest")]
		public ActionResult GetBadRequest()
		{
			return BadRequest();
		}

		[HttpGet("BadRequest/{id}")]
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}
	}
}
