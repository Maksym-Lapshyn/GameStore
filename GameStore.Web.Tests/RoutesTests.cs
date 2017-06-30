using System;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        public void CreateGame_CallsRightControllerAndAction_WhenValidRoutePassed()
        {
            TestRouteMatch("~/games/new", "Game", "NewGame", "POST");
        }

        [TestMethod]
        public void EditGame_CallsRightControllerAndAction_WhenValidRoutePassed()
        {
            TestRouteMatch("~/games/update", "Game", "UpdateGame", "POST");
        }

        [TestMethod]
        public void GetGameDetailsByKey_CallsRightControllerAndAction_WhenValidRoutePassed()
        {
            TestRouteMatch("~/game/{gameKey}", "Game", "ShowGame");
        }

        [TestMethod]
        public void GetAllGames_CallsRightControllerAndAction_WhenValidRoutePassed()
        {
            TestRouteMatch("~/games", "Game", "ListAllGames");
        }

        [TestMethod]
        public void DeleteGame_CallsRightControllerAndAction_WhenValidRoutePassed()
        {
            TestRouteMatch("~/games/remove", "Game", "DeleteGame", "POST");
        }

        [TestMethod]
        public void LeaveComment_CallsRightControllerAndAction_WhenValidRoutePassed()
        {
            TestRouteMatch("~/game/{gameKey}/newcomment", "Comment", "NewComment", "POST");
        }

        [TestMethod]
        public void GetAllComments_CallsRightControllerAndAction_WhenValidRoutePassed()
        {
            TestRouteMatch("~/game/{gameKey}/comments", "Comment", "ListAllComments");
        }

        [TestMethod]
        public void DownloadGame_CallsRightControllerAndAction_WhenValidRoutePassed()
        {
            TestRouteMatch("~/game/{gameKey}/download", "Game", "DownloadGame");
        }

        [TestMethod]
        public void DisplayPublisher_CallsRightControllerAndAction_WhenValidRoutePassed()
        {
            TestRouteMatch("~/publisher/{companyName}", "Publisher", "ShowPublisher");
        }
    }
}
