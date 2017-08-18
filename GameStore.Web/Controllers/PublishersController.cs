using AutoMapper;
using GameStore.Common.Enums;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Infrastructure.Attributes;
using GameStore.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	[CustomAuthorize(AuthorizationMode.Allow, AccessLevel.Manager)]
	public class PublishersController : BaseController
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
		public ActionResult New(PublisherViewModel model)
		{
			CheckIfCompanyNameIsUnique(model);

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var publisherDto = _mapper.Map<PublisherViewModel, PublisherDto>(model);
			_publisherService.Create(publisherDto);

			return RedirectToAction("ShowAll", "Publishers");
		}

		[HttpGet]
		public ActionResult Update(string key)
		{
			var publisherDto = _publisherService.GetSingle(key);
			var publisherViewModel = _mapper.Map<PublisherDto, PublisherViewModel>(publisherDto);

			return View(publisherViewModel);
		}

		[HttpPost]
		public ActionResult Update(PublisherViewModel model)
		{
			CheckIfCompanyNameIsUnique(model);

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var publisherDto = _mapper.Map<PublisherViewModel, PublisherDto>(model);
			_publisherService.Update(publisherDto);

			return RedirectToAction("ShowAll", "Publishers");
		}

		public ActionResult Show(string key)
		{
			var model = _mapper.Map<PublisherDto, PublisherViewModel>(_publisherService.GetSingle(key));

			return View(model);
		}

		[HttpPost]
		public ActionResult Delete(string key)
		{
			_publisherService.Delete(key);

			return RedirectToAction("ShowAll", "Publishers");
		}

		public ActionResult ShowAll()
		{
			var publisherDtos = _publisherService.GetAll();
			var model = _mapper.Map<IEnumerable<PublisherDto>, List<PublisherViewModel>>(publisherDtos);

			return View(model);
		}

		private void CheckIfCompanyNameIsUnique(PublisherViewModel model)
		{
			if (!_publisherService.Contains(model.CompanyName))
			{
				return;
			}

			var existingPublisher = _publisherService.GetSingle(model.CompanyName);

			if (existingPublisher.Id != model.Id)
			{
				ModelState.AddModelError("CompanyName", "Publisher with such company name already exists");
			}
		}
	}
}