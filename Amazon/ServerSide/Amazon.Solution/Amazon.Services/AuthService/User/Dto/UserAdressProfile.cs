using Amazon.Core.Entities.Identity;
using AutoMapper;

namespace Amazon.Services.AuthService.User.Dto
{
	public class UserAdressProfile :Profile
	{
		public UserAdressProfile() 
		{
			CreateMap<AddressDto, Address>();
			CreateMap<Address, AddressToReturnDto>();
		}
	}
}
