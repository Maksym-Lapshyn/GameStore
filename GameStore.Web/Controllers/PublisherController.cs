using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System.Net;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class PublisherController : Controller
	{
		private readonly IPublisherService _publisherService;

		public PublisherController(IPublisherService publisherService)
		{
			_publisherService = publisherService;
		}

		[HttpGet]
		public ActionResult New()
		{
			return View(new PublisherViewModel());
		}

		[HttpPost]
		public ActionResult New(PublisherViewModel publisherViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(publisherViewModel);
			}

			var publisherDto = Mapper.Map<PublisherViewModel, PublisherDto>(publisherViewModel);
			_publisherService.Create(publisherDto);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		public ActionResult Show(string companyName)
		{
			var publisherViewModel = Mapper.Map<PublisherDto, PublisherViewModel>(_publisherService.GetSingleBy(companyName));

			return View(publisherViewModel);
		}
	}
}