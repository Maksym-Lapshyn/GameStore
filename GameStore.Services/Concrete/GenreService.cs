using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Concrete
{
	public class GenreService : IGenreService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<GenreDto> GetAll()
		{
			var genres = _unitOfWork.GenreRepository.Get();
			var genreDtos = _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(genres);

			return genreDtos;
		}
	}
}