using System;
using System.Reflection;
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
            Mock<HttpRequestBase> mockOfRequest = new Mock<HttpRequestBase>();
            mockOfRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockOfRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            Mock<HttpResponseBase> mockOfResponse = new Mock<HttpResponseBase>();
            mockOfResponse = new Mock<HttpResponseBase>();
            mockOfResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            Mock<HttpContextBase> mockOfContext = new Mock<HttpContextBase>();
            mockOfContext.Setup(m => m.Request).Returns(mockOfRequest.Object);
            mockOfContext.Setup(m => m.Response).Returns(mockOfResponse.Object);

            return mockOfContext.Object;
        }

        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null,
            string httpMethod = "GET")
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action,
            object propertySet = null)
        {
            Func<object, object, bool> valCompare =
                (v1, v2) => StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            bool result = valCompare(routeResult.Values["controller"], controller) &&
                          valCompare(routeResult.Values["action"], action);
            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propInfo)
                {
                    if (
                        !(routeResult.Values.ContainsKey(pi.Name) &&
                          valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private void TestRouteFail(string url)
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData result = routes.GetRouteData(CreateHttpContext(url));
            Assert.IsTrue(result == null || result.Route == null);
        }

        [TestMethod]
        public void CreateGame_ValidRoute_BindsToControllerAndAction()
        {
            TestRouteMatch("~/games/new", "Game", "NewGame", null, "POST");
        }

        [TestMethod]
        public void EditGame_ValidRoute_BindsToControllerAndAction()
        {
            TestRouteMatch("~/games/update", "Game", "UpdateGame", null, "POST");
        }

        [TestMethod]
        public void GetGameDetailsByKey_ValidRoute_BindsToControllerAndAction()
        {
            TestRouteMatch("~/game/{gameKey}", "Game", "ShowGame");
        }

        [TestMethod]
        public void GetAllGames_ValidRoute_BindsToControllerAndAction()
        {
            TestRouteMatch("~/games", "Game", "ListAllGames");
        }

        [TestMethod]
        public void DeleteGame_ValidRoute_BindsToControllerAndAction()
        {
            TestRouteMatch("~/games/remove", "Game", "DeleteGame", null, "POST");
        }

        [TestMethod]
        public void LeaveComment_ValidRoute_BindsToControllerAndAction()
        {
            TestRouteMatch("~/game/{gameKey}/newcomment", "Comment", "NewComment", null, "POST");
        }

        [TestMethod]
        public void GetAllComments_ValidRoute_BindsToControllerAndAction()
        {
            TestRouteMatch("~/game/{gameKey}/comments", "Comment", "ListAllComments");
        }

        [TestMethod]
        public void DownloadGame_ValidRoute_BindsToControllerAndAction()
        {
            TestRouteMatch("~/game/{gameKey}/download", "Game", "DownloadGame");
        }
    }
}
