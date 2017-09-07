using AutoMapper;
using GameStore.Authentification.Abstract;
using GameStore.Common.App_LocalResources;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Web.Infrastructure.Attributes;
using GameStore.Web.Models;
using GameStore.Web.PaymentService;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class PaymentsController : BaseController
	{
		private const string GameStoreCardNumber = "5401323513514067";
		private const string PaymentPurpose = "Purchase of games from GameStore";

		private readonly IPaymentService _paymentService;
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public PaymentsController(IAuthentication authentication,
			IOrderService orderService,
			IMapper mapper)
			: base(authentication)
		{
			_paymentService = new PaymentServiceClient();
			_orderService = orderService;
			_mapper = mapper;
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.User)]
		public ActionResult Checkout(int orderId)
		{
			var order = _orderService.GetSingle(orderId);
			var model = new PaymentViewModel { PaymentAmount = order.TotalPrice };

			return View(model);
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.User)]
		[HttpPost]
		public ActionResult Checkout(PaymentViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			model.PaymentPurpose = PaymentPurpose;
			model.SellersCardNumber = GameStoreCardNumber;
			var dto = _mapper.Map<PaymentViewModel, Payment>(model);
			var response = _paymentService.ConductPurchase(dto);

			if (response.PaymentStatus == PaymentStatus.Pending)
			{
				return RedirectToAction("Confirm", new { paymentId = response.PaymentId });
			}

			if (response.PaymentStatus == PaymentStatus.Successful)
			{
				_orderService.CheckoutActive(CurrentUser.Id);

				return RedirectToAction("ShowAll", "Games");
			}

			ModelState.AddModelError("", GlobalResource.ResourceManager.GetString(response.PaymentStatus.ToString()));

			return View(model);
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.User)]
		public ActionResult Confirm(int paymentId)
		{
			var model = new ConfirmationViewModel { PaymentId = paymentId };

			return View(model);
		}

		[AuthorizeUser(AuthorizationMode.Allow, AccessLevel.User)]
		[HttpPost]
		public ActionResult Confirm(ConfirmationViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var response = _paymentService.ConfirmPayment(model.PaymentId, model.ConfirmationCode);

			if (response.PaymentStatus == PaymentStatus.Pending)
			{
				ModelState.AddModelError("ConfirmationCode", GlobalResource.ConfirmationCodeIsNotCorrect);

				return View(model);
			}

			if (response.PaymentStatus == PaymentStatus.Successful)
			{
				_orderService.CheckoutActive(CurrentUser.Id);

				return RedirectToAction("ShowAll", "Games");
			}

			ModelState.AddModelError("", GlobalResource.PaymentIdIsIncorrect);

			return View(model);
		}
	}
}