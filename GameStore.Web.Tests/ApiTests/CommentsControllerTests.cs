using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Controllers.Api;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;

namespace GameStore.Web.Tests.ApiTests
{
	[TestClass]
	public class CommentsControllerTests
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

		private Mock<ICommentService> _mockOfCommentService;
		private Mock<IApiAuthentication> _mockOfApiAuthentication;
		private CommentsController _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfCommentService = new Mock<ICommentService>();
			_mockOfApiAuthentication = new Mock<IApiAuthentication>();
			_target = new CommentsController(_mockOfApiAuthentication.Object, _mockOfCommentService.Object, _mapper);
		}

		[TestMethod]
		public void GetAllByGameKey_ReturnsHttpStatusCodeBadRequest_WhenCommentServiceDoesNotContainCommentByGameKey()
		{
			_mockOfCommentService.Setup(m => m.Contains(InvalidString)).Returns(false);

			var result = _target.GetAllByGameKey(ValidString, JsonContentType) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void GetAllByGameKey_ReturnsHttpStatusCodeBadRequest_WhenGameDoesNotContainComments()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.GetAll(ValidString)).Returns(new List<CommentDto>());

			var result = _target.GetAllByGameKey(ValidString, JsonContentType) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void GetAllByGameKey_ReturnsJson_WhenContentTypeIsJson()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.GetAll(ValidString)).Returns(new List<CommentDto>{new CommentDto()});

			var result = _target.GetAllByGameKey(ValidString, JsonContentType) as FormattedContentResult<List<CommentViewModel>>;

			Assert.AreEqual(JsonMediaType, result.MediaType.MediaType);
		}

		[TestMethod]
		public void GetAllByGameKey_ReturnsXml_WhenContentTypeIsXml()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.GetAll(ValidString)).Returns(new List<CommentDto> { new CommentDto() });

			var result = _target.GetAllByGameKey(ValidString, XmlContentType) as FormattedContentResult<List<CommentViewModel>>;

			Assert.AreEqual(XmlMediaType, result.MediaType.MediaType);
		}

		[TestMethod]
		public void Get_ReturnsHttpStatusCodeBadRequest_WhenGameDoesNotContainComments()
		{
			_mockOfCommentService.Setup(m => m.Contains(InvalidString)).Returns(false);

			var result = _target.GetAllByGameKey(InvalidString, JsonContentType) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void Get_ReturnsHttpStatusCodeBadRequest_WhenInvalidCommentIdIsPassed()
		{
			_mockOfCommentService.Setup(m => m.Contains(InvalidString)).Returns(false);
			_mockOfCommentService.Setup(m => m.Contains(InvalidInt)).Returns(false);

			var result = _target.Get(ValidString, InvalidInt, JsonContentType) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void Get_ReturnsJson_WhenContentTypeIsJson()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.Contains(ValidInt)).Returns(true);
			_mockOfCommentService.Setup(m => m.GetSingle(ValidInt)).Returns(new CommentDto());

			var result = _target.Get(ValidString, ValidInt, JsonContentType) as FormattedContentResult<CommentViewModel>;

			Assert.AreEqual(JsonMediaType, result.MediaType.MediaType);
		}

		[TestMethod]
		public void Get_ReturnsXml_WhenContentTypeIsXml()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.Contains(ValidInt)).Returns(true);
			_mockOfCommentService.Setup(m => m.GetSingle(ValidInt)).Returns(new CommentDto());

			var result = _target.Get(ValidString, ValidInt, XmlContentType) as FormattedContentResult<CommentViewModel>;

			Assert.AreEqual(XmlMediaType, result.MediaType.MediaType);
		}

		[TestMethod]
		public void Post_ReturnsStatusCodeBadRequest_WhenInvalidOrderIdIsPassed()
		{
			_mockOfCommentService.Setup(m => m.Contains(InvalidInt)).Returns(false);

			var result = _target.Post(InvalidString, new CommentViewModel()) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void Post_ReturnsErrorViewModel_WhenModelContainsErrors()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			var result = _target.Post(ValidString, new CommentViewModel()) as NegotiatedContentResult<ErrorViewModel>;

			Assert.IsInstanceOfType(result.Content, typeof(ErrorViewModel));
		}

		[TestMethod]
		public void Post_ReturnsStatusCodeBadRequest_WhenModelContainsErrors()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			var result = _target.Post(ValidString, new CommentViewModel()) as NegotiatedContentResult<ErrorViewModel>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void Post_CallsCreateOnce_WhenModelStateIsValid()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);

			_target.Post(ValidString, new CommentViewModel());

			_mockOfCommentService.Verify(m => m.Create(It.IsAny<CommentDto>()), Times.Once);
		}

		[TestMethod]
		public void Put_ReturnsStatusCodeBadRequest_WhenInvalidGameKeyIsPassed()
		{
			_mockOfCommentService.Setup(m => m.Contains(InvalidString)).Returns(false);

			var result = _target.Put(InvalidString, new CommentViewModel()) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void Put_ReturnsStatusCodeBadRequest_WhenCommentWithInvalidIdIsPassed()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.Contains(InvalidInt)).Returns(false);

			var result = _target.Put(ValidString, new CommentViewModel { Id = InvalidInt }) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void Put_ReturnsStatusCodeBadRequest_WhenModelContainsErrors()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.Contains(ValidInt)).Returns(true);
			_target.ModelState.AddModelError(InvalidString, InvalidString);

			var result = _target.Put (ValidString, new CommentViewModel { Id = ValidInt }) as NegotiatedContentResult<ErrorViewModel>;

			Assert.IsInstanceOfType(result.Content, typeof(ErrorViewModel));
		}

		[TestMethod]
		public void Put_CallsUpdateOnce_WhenModelStateIsValid()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.Contains(ValidInt)).Returns(true);

			var result = _target.Put(ValidString, new CommentViewModel { Id = ValidInt }) as NegotiatedContentResult<ErrorViewModel>;

			_mockOfCommentService.Verify(m => m.Update(It.IsAny<CommentDto>()), Times.Once);
		}

		[TestMethod]
		public void Delete_ReturnsStatusCodeBadRequest_WhenInvalidGameKeyIsPassed()
		{
			_mockOfCommentService.Setup(m => m.Contains(InvalidString)).Returns(false);

			var result = _target.Delete(InvalidString, ValidInt) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void Delete_ReturnsStatusCodeBadRequest_WhenCommentWithInvalidIdIsPassed()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.Contains(InvalidInt)).Returns(false);

			var result = _target.Delete(ValidString, InvalidInt) as NegotiatedContentResult<string>;

			Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
		}

		[TestMethod]
		public void Delete_CallsDeleteOnce_WhenModelStateIsValid()
		{
			_mockOfCommentService.Setup(m => m.Contains(ValidString)).Returns(true);
			_mockOfCommentService.Setup(m => m.Contains(ValidInt)).Returns(true);

			var result = _target.Delete(ValidString, ValidInt) as NegotiatedContentResult<string>;

			_mockOfCommentService.Verify(m => m.Delete(ValidInt), Times.Once);
		}
	}
}