using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System;
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

        public void Create(GenreDto entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(GenreDto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public GenreDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenreDto> GetAll()
        {
            var genres = _unitOfWork.GenreRepository.Get();
            var genreDtos = Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(genres);

            return genreDtos;
        }

        public IEnumerable<GenreDto> GetBy(int gameId)
        {
            var allGenres = _unitOfWork.GenreRepository.Get();
            var matchedGenres = new List<Genre>();
            foreach (var genre in allGenres)
            {
                foreach (var game in genre.Games)
                {
                    if (game.Id == gameId)
                    {
                        matchedGenres.Add(genre);
                    }  
                }
            }
            
            var genreDtos = Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDto>>(matchedGenres);

            return genreDtos;
        }
    }
}