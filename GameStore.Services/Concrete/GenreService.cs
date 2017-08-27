using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Concrete
{
	public class GenreService : IGenreService
	{
        private const string DefaultLanguage = "en";

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IInputLocalizer<Genre> _localizer;
        private readonly IGenreRepository _genreRepository;
		private readonly IGameRepository _gameRepository;


		public GenreService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IInputLocalizer<Genre> localizer,
			IGenreRepository genreRepository,
			IGameRepository gameRepository
			)
		{
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
			_genreRepository = genreRepository;
			_gameRepository = gameRepository;
		}

		public IEnumerable<GenreDto> GetAll(string language)
		{
			var genres = _genreRepository.GetAll(language);
			var genreDtos = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(genres);

			return genreDtos;
		}

		public GenreDto GetSingle(string language, string name)
		{
			var genre = _genreRepository.GetSingle(language, g => g.Name.ToLower() == name.ToLower());
			var genreDto = _mapper.Map<Genre, GenreDto>(genre);

			return genreDto;
		}

		public void Create(string language, GenreDto genreDto)
		{
			var genre = _mapper.Map<GenreDto, Genre>(genreDto);
			genre = MapEmbeddedEntities(genreDto, genre);
            genre = _localizer.Localize(language, genre);
            _genreRepository.Insert(genre);
			_unitOfWork.Save();
		}

		public void Update(string language, GenreDto genreDto)
		{
			var genre = _genreRepository.GetSingle(DefaultLanguage, g => g.Id == genreDto.Id);
			genre = _mapper.Map(genreDto, genre);
			genre = MapEmbeddedEntities(genreDto, genre);
            genre = _localizer.Localize(language, genre);
            _genreRepository.Update(genre);
			_unitOfWork.Save();
		}

		public void Delete(string name)
		{
			_genreRepository.Delete(name);
			_unitOfWork.Save();
		}

		public bool Contains(string name)
		{
			return _genreRepository.Contains(g => g.Name == name);
		}

		private Genre MapEmbeddedEntities(GenreDto input, Genre result)
		{
			if (input.ParentGenreInput != null)
			{
				result.ParentGenre = _genreRepository.GetSingle(DefaultLanguage, g => g.Name == input.ParentGenreInput);
			}

			return result;
		}
	}
}