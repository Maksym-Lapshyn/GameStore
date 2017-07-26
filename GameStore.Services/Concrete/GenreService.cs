using AutoMapper;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class GenreService : IGenreService
	{
		private readonly IMapper _mapper;
		private readonly IEfGenreRepository _genreRepository;

		public GenreService(IMapper mapper,
			IEfGenreRepository genreRepository)
		{
			_mapper = mapper;
			_genreRepository = genreRepository;
		}

		public IEnumerable<GenreDto> GetAll()
		{
			var genres = _genreRepository.Get();
			var genreDtos = _mapper.Map<IQueryable<Genre>, IEnumerable<GenreDto>>(genres);

			return genreDtos;
		}
	}
}