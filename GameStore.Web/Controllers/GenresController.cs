using System.Collections.Generic;
using AutoMapper;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using GameStore.Web.Models;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
	public class GenresController : BaseController
	{
		private readonly IGenreService _genreService;
		private readonly IMapper _mapper;

		public GenresController(IGenreService genreService,
			IMapper mapper)
		{
			_genreService = genreService;
			_mapper = mapper;
		}

		[HttpGet]
		public ActionResult New()
		{
			var model = new GenreViewModel {ParentGenreData = new List<GenreViewModel>()};

			return View(model);
		}

		[HttpPost]
		public ActionResult New(GenreViewModel model)
		{
			CheckIfNameIsUnique(model);
			if (!ModelState.IsValid)
			{
				model.ParentGenreData = GetGenres();
				return View(model);
			}

			var genreDto = _mapper.Map<GenreViewModel, GenreDto>(model);
			_genreService.Create(genreDto);

			return RedirectToAction("ShowAll", "Games");
		}

		[HttpGet]
		public ActionResult Update(string key)
		{
			var genreDto = _genreService.GetSingle(key);
			var model = _mapper.Map<GenreDto, GenreViewModel>(genreDto);
			model.ParentGenreData = GetGenres();

			return View(model);
		}

		[HttpPost]
		public ActionResult Update(GenreViewModel model)
		{
			CheckIfNameIsUnique(model);
			if (!ModelState.IsValid)
			{
				model.ParentGenreData = GetGenres();
				return View(model);
			}

			var genreDto = _mapper.Map<GenreViewModel, GenreDto>(model);
			_genreService.Update(genreDto);

			return RedirectToAction("ShowAll", "Games");
		}

		public ActionResult Show(string key)
		{
			var genreDto = _genreService.GetSingle(key);
			var model = _mapper.Map<GenreDto, GenreViewModel>(genreDto);

			return View(model);
		}

		public ActionResult Delete(string key)
		{
			_genreService.Delete(key);

			return RedirectToAction("ShowAll", "Games");
		}

		public ActionResult ShowAll()
		{
			var genreDtos = _genreService.GetAll();
			var genreViewModels = _mapper.Map<IEnumerable<GenreDto>, List<GenreViewModel>>(genreDtos);

			return View(genreViewModels);
		}

		private List<GenreViewModel> GetGenres()
		{
			return _mapper.Map<IEnumerable<GenreDto>, List<GenreViewModel>>(_genreService.GetAll());
		}

		private void CheckIfNameIsUnique(GenreViewModel model)
		{
			if (!_genreService.Contains(model.Name))
			{
				return;
			}

			var existingGenre = _genreService.GetSingle(model.Name);

			if (existingGenre.Id != model.Id)
			{
				ModelState.AddModelError("Name", "Genre with such name already exists");
			}
		}
	}
}