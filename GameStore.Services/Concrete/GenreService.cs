using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GenreService : IGenreService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IInputLocalizer<Genre> _inputLocalizer;
		private readonly IOutputLocalizer<Genre> _outputLocalizer;
		private readonly IGenreRepository _genreRepository;


		public GenreService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IInputLocalizer<Genre> inputLocalizer,
			IOutputLocalizer<Genre> outputLocalizer,
			IGenreRepository genreRepository
			)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_inputLocalizer = inputLocalizer;
			_outputLocalizer = outputLocalizer;
			_genreRepository = genreRepository;
		}

		public IEnumerable<GenreDto> GetAll(string language)
		{
			var genres = _genreRepository.GetAll().ToList();

			foreach (var genre in genres)
			{
				_outputLocalizer.Localize(language, genre);
			}

			var genreDtos = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(genres);

			return genreDtos;
		}

		public GenreDto GetSingle(string language, string name)
		{
			var genre = _genreRepository.GetSingle(g => g.GenreLocales.Any(l => l.Name == name) || g.Name == name);
			_outputLocalizer.Localize(language, genre);
			var genreDto = _mapper.Map<Genre, GenreDto>(genre);

			return genreDto;
		}

		public void Create(string language, GenreDto genreDto)
		{
			var genre = _mapper.Map<GenreDto, Genre>(genreDto);
			genre = MapEmbeddedEntities(genreDto, genre);
			_inputLocalizer.Localize(language, genre);
			_genreRepository.Insert(genre);
			_unitOfWork.Save();
		}

		public void Update(string language, GenreDto genreDto)
		{
			var genre = _genreRepository.GetSingle(g => g.Id == genreDto.Id);
			genre = _mapper.Map(genreDto, genre);
			genre = MapEmbeddedEntities(genreDto, genre);
			_inputLocalizer.Localize(language, genre);
			_genreRepository.Update(genre);
			_unitOfWork.Save();
		}

		public void Delete(string name)
		{
			_genreRepository.Delete(name);
			_unitOfWork.Save();
		}

		public bool Contains(string language, string name)
		{
			return _genreRepository.Contains(g => g.GenreLocales.Any(l => l.Language.Name == language && l.Name == name));
		}

		private Genre MapEmbeddedEntities(GenreDto input, Genre result)
		{
			if (input.ParentGenreInput != null)
			{
				result.ParentGenre = _genreRepository.GetSingle(g => g.GenreLocales.Any(l => l.Name == input.ParentGenreInput) || g.Name == input.ParentGenreInput);
			}

			return result;
		}
	}
}