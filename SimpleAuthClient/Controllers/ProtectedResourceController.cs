using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SimpleAuthClient.Controllers
{
	[Authorize]
	[Route("api/ProtectedResource")]
	[ApiController]
	public class ProtectedResourceController : ControllerBase
	{

		[Authorize(Roles = "DesktopAdmin1")]
		[HttpGet("Endpoint1")]
		public dynamic GetEndpoint1()
		{
			return DisplayDetails();
		}

		[Authorize(Roles = "DesktopAdmin2")]
		[HttpGet("Endpoint2")]
		public dynamic GetEndpoint2()
		{
			return DisplayDetails();
		}

		[Authorize(Roles = "DesktopAdmin3")]
		[HttpGet("Endpoint3")]
		public dynamic GetEndpoint3()
		{
			return DisplayDetails();
		}

		// Roles authorized in "Or"
		[Authorize(Roles = "DesktopAdmin1, DesktopAdmin2")]
		[HttpGet("Endpoint4")]
		public dynamic GetEndpoint4()
		{
			return DisplayDetails();
		}

		// Roles authorized in "And"
		[Authorize(Roles = "DesktopAdmin1")]
		[Authorize(Roles = "DesktopAdmin2")]
		[HttpGet("Endpoint5")]
		public dynamic GetEndpoint5()
		{
			return DisplayDetails();
		}

		[HttpGet("Authentication/Me")]
		public dynamic Me()
		{
			return DisplayDetails();
		}

		private dynamic DisplayDetails() => new
		{
			Name = User.Identity.Name,
			Surname = User.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").FirstOrDefault().Value,
			LanId = User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname").FirstOrDefault().Value,
			Roles = User.Claims.Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(r => r.Value).ToList(),
			IsAuthenticated = User.Identity.IsAuthenticated,
		};
	}
}
