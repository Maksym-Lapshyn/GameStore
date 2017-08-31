using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;
using GameStore.Web.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameStore.Web.Controllers.Api
{
	public class BaseApiController : ApiController
	{
		private readonly IApiAuthentication _authentication;

		private string _currentLanguage;

		public BaseApiController(IApiAuthentication authentication)
		{
			_authentication = authentication;
		}

		public string CurrentLanguage => _currentLanguage ?? (_currentLanguage = ControllerContext.RouteData.Values["lang"]?.ToString() ?? "en");

		public User CurrentUser => _authentication.CurrentUser;

		[HttpPost]
		public HttpResponseMessage Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, CreateError());
			}

			var token = _authentication.LogIn(model.Login, model.Password, model.IsPersistent);

			return token == null 
				? Request.CreateResponse(HttpStatusCode.Forbidden, "There is no such user") 
				: Request.CreateResponse(HttpStatusCode.OK, token);
		}

		private ErrorViewModel CreateError()
		{
			var error = new ErrorViewModel
			{
				Message = "ModelState contains errors",
				Errors = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
			};

			return error;
		}
	}
}