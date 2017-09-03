using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;
using GameStore.Web.Models;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Results;

namespace GameStore.Web.Controllers.Api
{
	public class BaseApiController : ApiController
	{
        private const string DefaultLanguage = "en";
		private const string XmlContentType = "xml";
		private const string JsonContentType = "json";
		private const string JsonMediaType = "application/json";
		private const string XmlMediaType = "application/xml";

		private readonly IApiAuthentication _authentication;

		private string _currentLanguage;

		public BaseApiController(IApiAuthentication authentication)
		{
			_authentication = authentication;
		}

		public string CurrentLanguage => _currentLanguage ?? (_currentLanguage = ControllerContext.RouteData?.Values["lang"]?.ToString() ?? DefaultLanguage);

		public User CurrentUser => _authentication.CurrentUser;

		[HttpPost]
		public IHttpActionResult Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return Content(HttpStatusCode.BadRequest, CreateError());
			}

			var token = _authentication.LogIn(model.Login, model.Password, model.IsPersistent);

			if (token == null)
			{
				return Content(HttpStatusCode.BadRequest, "There is no such user");
			}

			return Ok(token);
		}

		protected FormattedContentResult<T> SerializeResult<T>(T content, string contentType)
		{
			FormattedContentResult<T> result = null;

			if (contentType == JsonContentType)
			{
				result = new FormattedContentResult<T>(
					HttpStatusCode.OK,
					content,
					new JsonMediaTypeFormatter(),
					new MediaTypeHeaderValue(JsonMediaType),
					this
				);
			}

			if (contentType == XmlContentType)
			{
				result = new FormattedContentResult<T>(
					HttpStatusCode.OK,
					content,
					new XmlMediaTypeFormatter(),
					new MediaTypeHeaderValue(XmlMediaType),
					this
				);
			}

			return result;
		}

		protected ErrorViewModel CreateError()
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