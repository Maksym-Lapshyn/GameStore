using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.Models;
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

			return RedirectToAction("ListAll", "Games");
		}

		[HttpGet]
		public ActionResult Update(string key)
		{
			var publisherDto = _publisherService.GetSingleBy(key);
			var publisherViewModel = _mapper.Map<PublisherDto, PublisherViewModel>(publisherDto);

			return View(publisherViewModel);
		}

		[HttpPost]
		public ActionResult Update(PublisherViewModel publisherViewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(publisherViewModel);
			}

			var publisherDto = _mapper.Map<PublisherViewModel, PublisherDto>(publisherViewModel);
			_publisherService.Update(publisherDto);

			return RedirectToAction("ListAll", "Games");
		}

		public ActionResult Show(string key)
		{
			var publisherViewModel = _mapper.Map<PublisherDto, PublisherViewModel>(_publisherService.GetSingleBy(key));

			return View(publisherViewModel);
		}
	}
}