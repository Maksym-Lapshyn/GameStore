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

		public GenreService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<GenreDto> GetAll()
		{
			var genres = _unitOfWork.GenreRepository.Get();
			var genreDtos = Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(genres);

			return genreDtos;
		}
	}
}