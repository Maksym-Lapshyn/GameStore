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

		public GenreService(IMapper mapper,
			IGenreRepository genreRepository)
		{
			_mapper = mapper;
			_genreRepository = genreRepository;
		}

		public IEnumerable<GenreDto> GetAll()
		{
			var genres = _genreRepository.Get();
			var genreDtos = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(genres);

			return genreDtos;
		}
	}
}