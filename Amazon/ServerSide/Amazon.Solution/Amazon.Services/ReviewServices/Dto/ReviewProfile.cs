using Amazon.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ReviewServices.Dto
{
	public class ReviewProfile : Profile
	{
		public ReviewProfile()
		{
			CreateMap<ReviewDto,Review >();
			CreateMap<Review, ReviewToReturnDto>();
		}
	}
}
