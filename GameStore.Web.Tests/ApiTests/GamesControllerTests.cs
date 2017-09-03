using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Controllers.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using GameStore.Web.Models;
using System.Web.Http.Routing;

namespace GameStore.Web.Tests.ApiTests
{
	[TestClass]
	public class GamesControllerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";
		private const int ValidInt = 1;
		private const int InvalidInt = 10;
		private const string XmlContentType = "xml";
		private const string JsonContentType = "json";
		private const string JsonMediaType = "application/json";
		private const string XmlMediaType = "application/xml";

		private readonly IMapper _mapper = new Mapper(
			new MapperConfiguration(cfg => cfg.AddProfile(new WebProfile())));

		private Mock<IGameService> _mockOfGameService;
        private Mock<IGenreService> _mockOfGenreService;
        private Mock<IPublisherService> _mockOfPublisherService;
        private Mock<IPlatformTypeService> _mockOfPlatformTypeService;
		private Mock<IApiAuthentication> _mockOfApiAuthentication;
		private GamesController _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfGameService = new Mock<IGameService>();
            _mockOfGenreService = new Mock<IGenreService>();
            _mockOfPublisherService = new Mock<IPublisherService>();
            _mockOfPlatformTypeService = new Mock<IPlatformTypeService>();
			_mockOfApiAuthentication = new Mock<IApiAuthentication>();
			_target = new GamesController(_mockOfApiAuthentication.Object, _mockOfGameService.Object, _mockOfGenreService.Object, _mockOfPublisherService.Object, _mockOfPlatformTypeService.Object, _mapper);
		}

		[TestMethod]
		public void GetAllByCompanyName_ReturnsHttpStatusCodeBadRequest_WhenInvalidCompanyNameIsPassed()
		{
			_mockOfPublisherService.Setup(m => m.Contains(InvalidString)).Returns(false);

			var result = _target.GetAllByCompanyName(InvalidString, JsonContentType) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

        [TestMethod]
        public void GetAllByCompanyName_ReturnsJson_WhenContentTypeIsJson()
        {
            _mockOfPublisherService.Setup(m => m.Contains(ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetAllByCompanyName(It.IsAny<string>(), ValidString)).Returns(new List<GameDto>());

            var result = _target.GetAllByCompanyName(ValidString, JsonContentType) as FormattedContentResult<IEnumerable<GameViewModel>>;

            Assert.AreEqual(JsonMediaType, result.MediaType.MediaType);
        }

        [TestMethod]
        public void GetAllByCompanyName_ReturnsXml_WhenContentTypeIsXml()
        {
            _mockOfPublisherService.Setup(m => m.Contains(ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetAllByCompanyName(It.IsAny<string>(), ValidString)).Returns(new List<GameDto>());

            var result = _target.GetAllByCompanyName(ValidString, XmlContentType) as FormattedContentResult<IEnumerable<GameViewModel>>;

            Assert.AreEqual(XmlMediaType, result.MediaType.MediaType);
        }

        [TestMethod]
        public void GetAllByGenreName_ReturnsHttpStatusCodeBadRequest_WhenInvalidCompanyNameIsPassed()
        {
            _mockOfGenreService.Setup(m => m.Contains(It.IsAny<string>(), InvalidString)).Returns(false);

            var result = _target.GetAllByGenreName(InvalidString, JsonContentType) as NegotiatedContentResult<string>;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void GetAllByGenreName_ReturnsJson_WhenContentTypeIsJson()
        {
            _mockOfGenreService.Setup(m => m.Contains(It.IsAny<string>(), ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetAllByGenreName(It.IsAny<string>(), ValidString)).Returns(new List<GameDto>());

            var result = _target.GetAllByGenreName(ValidString, JsonContentType) as FormattedContentResult<IEnumerable<GameViewModel>>;

            Assert.AreEqual(JsonMediaType, result.MediaType.MediaType);
        }

        [TestMethod]
        public void GetAllByGenreName_ReturnsXml_WhenContentTypeIsXml()
        {
            _mockOfGenreService.Setup(m => m.Contains(It.IsAny<string>(), ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetAllByGenreName(It.IsAny<string>(), ValidString)).Returns(new List<GameDto>());

            var result = _target.GetAllByGenreName(ValidString, XmlContentType) as FormattedContentResult<IEnumerable<GameViewModel>>;

            Assert.AreEqual(XmlMediaType, result.MediaType.MediaType);
        }

        [TestMethod]
		public void Get_ReturnsHttpStatusCodeBadRequest_WhenModelStateIsInvalidAndFilterIsChanged()
		{
            _target.ModelState.AddModelError(InvalidString, InvalidString);

            var result = _target.Get(new CompositeGamesViewModel { FilterIsChanged = true }, JsonContentType) as NegotiatedContentResult<ErrorViewModel>;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

		[TestMethod]
		public void Get_ReturnsHttpStatusCodeBadRequest_WhenInvalidGameKeyIsPassed()
		{
			_mockOfGameService.Setup(m => m.Contains(InvalidString)).Returns(false);

			var result = _target.Get(InvalidString, JsonContentType) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

        [TestMethod]
        public void Get_ReturnsJson_WhenContentTypeIsJson()
        {
            _mockOfGameService.Setup(m => m.Contains(ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetSingle(It.IsAny<string>(), ValidString)).Returns(new GameDto());

            var result = _target.Get(ValidString, JsonContentType) as FormattedContentResult<GameViewModel>;

            Assert.AreEqual(JsonMediaType, result.MediaType.MediaType);
        }

        [TestMethod]
        public void Get_ReturnsXml_WhenContentTypeIsXml()
        {
            _mockOfGameService.Setup(m => m.Contains(ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetSingle(It.IsAny<string>(), ValidString)).Returns(new GameDto());

            var result = _target.Get(ValidString, XmlContentType) as FormattedContentResult<GameViewModel>;

            Assert.AreEqual(XmlMediaType, result.MediaType.MediaType);
        }

        [TestMethod]
        public void Put_ReturnsStatusCodeBadRequest_WhenGameWithInvalidKeyIsPassed()
        {
            _mockOfGameService.Setup(m => m.Contains(InvalidString)).Returns(false);

            var result = _target.Put(new GameViewModel { Key = InvalidString }) as NegotiatedContentResult<string>;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Put_ReturnsErrorViewModel_WhenModelContainsErrors()
        {
            _mockOfGameService.Setup(m => m.Contains(ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetSingle(It.IsAny<string>(), ValidString)).Returns(new GameDto());
            _target.ModelState.AddModelError(InvalidString, InvalidString);

            var result = _target.Put(new GameViewModel { Key = ValidString }) as NegotiatedContentResult<ErrorViewModel>;

            Assert.IsInstanceOfType(result.Content, typeof(ErrorViewModel));
        }

        [TestMethod]
        public void Put_CallsUpdateOnce_WhenModelStateIsValid()
        {
            _mockOfGameService.Setup(m => m.Contains(ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetSingle(It.IsAny<string>(), ValidString)).Returns(new GameDto());

            _target.Put(new GameViewModel { Key = ValidString });

            _mockOfGameService.Verify(m => m.Update(It.IsAny<string>(), It.IsAny<GameDto>()), Times.Once);
        }

        [TestMethod]
        public void Post_ReturnsStatusCodeBadRequest_WhenModelStateIsInvalid()
        {
            _mockOfGameService.Setup(m => m.Contains(InvalidString)).Returns(false);
            _target.ModelState.AddModelError(InvalidString, InvalidString);

            var result = _target.Post(new GameViewModel { Key = InvalidString }) as NegotiatedContentResult<ErrorViewModel>;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Post_CallsCreateOnce_WhenModelStateIsValid()
        {
            _mockOfGameService.Setup(m => m.Contains(ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetSingle(It.IsAny<string>(), ValidString)).Returns(new GameDto());

            _target.Post(new GameViewModel { Key = ValidString });

            _mockOfGameService.Verify(m => m.Create(It.IsAny<string>(), It.IsAny<GameDto>()), Times.Once);
        }

        [TestMethod]
        public void Delete_ReturnsStatusCodeBadRequest_WhenInvalidGameKeyIsPassed()
        {
            _mockOfGameService.Setup(m => m.Contains(InvalidString)).Returns(false);

            var result = _target.Delete(InvalidString) as NegotiatedContentResult<string>;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Delete_CallsDeleteOnce_WhenModelStateIsValid()
        {
            _mockOfGameService.Setup(m => m.Contains(ValidString)).Returns(true);
            _mockOfGameService.Setup(m => m.GetSingle(It.IsAny<string>(), ValidString)).Returns(new GameDto());

            var result = _target.Delete(ValidString) as NegotiatedContentResult<string>;

            _mockOfGameService.Verify(m => m.Delete(ValidString), Times.Once);
        }
    }
}