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
		private readonly IMapper _mapper;
		private readonly IGenreRepository _genreRepository;
		private readonly IGameRepository _gameRepository;
		private readonly IUnitOfWork _unitOfWork;

		public GenreService(IMapper mapper,
			IGenreRepository genreRepository,
			IGameRepository gameRepository,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_genreRepository = genreRepository;
			_gameRepository = gameRepository;
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<GenreDto> GetAll()
		{
			var genres = _genreRepository.GetAll();
			var genreDtos = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(genres);

			return genreDtos;
		}

		public GenreDto GetSingle(string name)
		{
			var genre = _genreRepository.GetSingle(g => g.Name == name);
			var genreDto = _mapper.Map<Genre, GenreDto>(genre);

			return genreDto;
		}

		public void Create(GenreDto genreDto)
		{
			var genre = _mapper.Map<GenreDto, Genre>(genreDto);
			genre = MapEmbeddedEntities(genreDto, genre);
			_genreRepository.Insert(genre);
			_unitOfWork.Save();
		}

		public void Update(GenreDto genreDto)
		{
			var genre = _genreRepository.GetSingle(g => g.Id == genreDto.Id);
			genre = _mapper.Map(genreDto, genre);
            genre = MapEmbeddedEntities(genreDto, genre);
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
				result.ParentGenre = _genreRepository.GetSingle(g => g.Name == input.ParentGenreInput);
			}

			return result;
		}
	}
}