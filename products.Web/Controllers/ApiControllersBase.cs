using MediatR;
using Microsoft.AspNetCore.Mvc;
using products.Models;
using System.Net;
using System.Security.Claims;

namespace products.Controllers
{
    [ApiController]
    public class ApiControllersBase : ControllerBase
    {

		public string CurrentUserId
        {
            get
            {
                return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        public string CurrentUserName
        {
            get
            {
                return User.FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string CurrentUserEmail
        {
            get
            {
                return User.FindFirst(ClaimTypes.Email).Value;
            }
        }

		

	}
}
