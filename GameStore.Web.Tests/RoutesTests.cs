using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web;
using System.Web.Routing;

namespace GameStore.Web.Tests
{
	[TestClass]
	public class RoutesTests
	{
		private HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
		{
			var mockOfRequest = new Mock<HttpRequestBase>();
			mockOfRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
			mockOfRequest.Setup(m => m.HttpMethod).Returns(httpMethod);
			var mockOfResponse = new Mock<HttpResponseBase>();
			mockOfResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);
			var mockOfContext = new Mock<HttpContextBase>();
			mockOfContext.Setup(m => m.Request).Returns(mockOfRequest.Object);
			mockOfContext.Setup(m => m.Response).Returns(mockOfResponse.Object);

			return mockOfContext.Object;
		}

		private void TestRouteMatch(string url, string controller, string action, string httpMethod = "GET")
		{
			var routes = new RouteCollection();
			RouteConfig.RegisterRoutes(routes);
			var result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

			var controllerAndActionMatched = result != null && (CompareValues(result.Values["controller"], controller) &&
																 CompareValues(result.Values["action"], action));

			Assert.IsNotNull(result);
			Assert.IsTrue(controllerAndActionMatched);
		}

		private bool CompareValues(object first, object second)
		{
			var result = StringComparer.InvariantCultureIgnoreCase.Compare(first, second) == 0;

			return result;
		}

		[TestMethod]
		public void CreateGame_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/games/new", "Games", "New", "POST");
		}

		[TestMethod]
		public void EditGame_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/games/update", "Games", "Update", "POST");
		}

		[TestMethod]
		public void GetGameDetailsByKey_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/games/{gameKey}", "Games", "Show");
		}

		[TestMethod]
		public void GetAllGames_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/games", "Games", "ListAll");
		}

		[TestMethod]
		public void DeleteGame_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/games/remove", "Games", "Remove", "POST");
		}

		[TestMethod]
		public void GetAllComments_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/games/{gameKey}/comments", "Comments", "NewComment");
		}

		[TestMethod]
		public void DownloadGame_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/games/{gameKey}/download", "Games", "Download");
		}

		[TestMethod]
		public void DisplayPublisher_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/publishers/{companyName}", "Publishers", "Show");
		}

		[TestMethod]
		public void CreatePublisher_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/publishers/new", "Publishers", "New");
		}

		[TestMethod]
		public void DisplayBasket_CallsRightControllerAndAction_WhenValidRouteIsPassed()
		{
			TestRouteMatch("~/busket", "Orders", "Show");
		}
	}
}
