using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.Services.Abstract;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        private Mock<IOrderService> _mockOfOrderService;
        private HttpCookieCollection _cookies;
        private OrderController _target;

        [TestInitialize]
        public void Initialize()
        {
            _mockOfOrderService = new Mock<IOrderService>();
            _target = new OrderController(_mockOfOrderService.Object);
            _cookies = new HttpCookieCollection();
            var response = new Mock<HttpResponseBase>();
            response.Setup(m => m.Cookies.Add(It.IsAny<HttpCookie>()))
                .Callback<HttpCookie>(c => _cookies.Add(c));
            var request = new Mock<HttpRequestBase>();
            request.Setup(m => m.Cookies).Returns(_cookies);
            var context = new Mock<HttpContextBase>(request.Object, response.Object);
            context.Setup(m => m.Request).Returns(request.Object);
            context.Setup(m => m.Response).Returns(response.Object);
            _target.ControllerContext = new ControllerContext(context.Object, new RouteData(), _target);
        }
    }
}

