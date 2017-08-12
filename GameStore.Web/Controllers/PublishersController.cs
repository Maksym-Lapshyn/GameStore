using System.Collections.Generic;
using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
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
            CheckIfCompanyNameIsUnique(model.CompanyName);

            if (!ModelState.IsValid)
			{
				return View(model);
			}

			var publisherDto = _mapper.Map<PublisherViewModel, PublisherDto>(model);
			_publisherService.Create(publisherDto);

			return RedirectToAction("ShowAll", "Games");
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
            CheckIfCompanyNameIsUnique(model.CompanyName);

            if (!ModelState.IsValid)
			{
				return View(model);
			}

			var publisherDto = _mapper.Map<PublisherViewModel, PublisherDto>(model);
			_publisherService.Update(publisherDto);

			return RedirectToAction("ShowAll", "Games");
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

			return RedirectToAction("ShowAll", "Games");
		}

		public ActionResult ShowAll()
		{
			var publisherDtos = _publisherService.GetAll();
			var model = _mapper.Map<IEnumerable<PublisherDto>, List<PublisherViewModel>>(publisherDtos);

			return View(model);
		}

        private void CheckIfCompanyNameIsUnique(string companyName)
        {
            if (!_publisherService.Contains(companyName))
            {
                return;
            }

            ModelState.AddModelError("CompanyName", "Publisher with such company name already exists");
        }
    }
}