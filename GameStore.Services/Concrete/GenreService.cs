using AutoMapper;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Concrete
{
	public class GenreService : IGenreService
	{
		private readonly IMapper _mapper;
		private readonly IGenreRepository _genreRepository;
		private readonly IUnitOfWork _unitOfWork;

		public GenreService(IMapper mapper,
			IGenreRepository genreRepository,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_genreRepository = genreRepository;
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
			var genre = _genreRepository.GetSingle(name);
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
			var genre = _genreRepository.GetSingle(genreDto.Name);
			genre = MapEmbeddedEntities(genreDto, genre);
			genre = _mapper.Map(genreDto, genre);
			_genreRepository.Update(genre);
			_unitOfWork.Save();
		}

		public void Delete(string name)
		{
			_genreRepository.Delete(name);
			_unitOfWork.Save();
		}

		private Genre MapEmbeddedEntities(GenreDto input, Genre result)
		{
			if (input.ParentGenreInput != null)
			{
				result.ParentGenre = _genreRepository.GetSingle(input.ParentGenreInput);
			}

			return result;
		}
	}
}