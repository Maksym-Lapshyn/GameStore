using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
using System.Net;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class PublishersController : Controller
	{
		private readonly IPublisherService _publisherService;
		private readonly IMapper _mapper;

		public PublishersController(IPublisherService publisherService,
			IMapper mapper)
		{
			_publisherService = publisherService;
			_mapper = mapper;
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

			var publisherDto = _mapper.Map<PublisherViewModel, PublisherDto>(publisherViewModel);
			_publisherService.Create(publisherDto);

			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}

		public ActionResult Show(string companyName)
		{
			var publisherViewModel = _mapper.Map<PublisherDto, PublisherViewModel>(_publisherService.GetSingleBy(companyName));

			return View(publisherViewModel);
		}
	}
}