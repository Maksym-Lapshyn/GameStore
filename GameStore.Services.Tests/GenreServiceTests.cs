﻿using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Services.Tests
{
    [TestClass]
    public class GenreServiceTests
    {
        private Mock<IUnitOfWork> _unitOfUow;
        private GenreService _target;
        private const int ValidInt = 10;
        private const int InvalidInt = 20;
        private List<Genre> _genres;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfUow = new Mock<IUnitOfWork>();
            _target = new GenreService(_unitOfUow.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsAllGenres()
        {
            _genres = new List<Genre>
            {
                new Genre(),
                new Genre(),
                new Genre()
            };

            _unitOfUow.Setup(m => m.GenreRepository.Get(null, null)).Returns(_genres);

            var result = _target.GetAll().ToList().Count;

            Assert.IsTrue(result == 3);
        }

        [TestMethod]
        public void GetBy_ReturnsAllGenres_WhenValidGameIdIsPassed()
        {
            _genres = new List<Genre>
            {
                new Genre{Games = new List<Game>{new Game{Id = ValidInt}}},
                new Genre{Games = new List<Game>{new Game{Id = ValidInt}}},
                new Genre{Games = new List<Game>{new Game{Id = ValidInt}}}
            };

            _unitOfUow.Setup(m => m.GenreRepository.Get(null, null)).Returns(_genres);

            var result = _target.GetBy(ValidInt).ToList().Count;

            Assert.IsTrue(result == 3);
        }

        [TestMethod]
        public void GetBy_ReturnsAllGenres_WhenInValidGameIdIsPassed()
        {
            _genres = new List<Genre>
            {
                new Genre{Games = new List<Game>{new Game{Id = ValidInt}}},
                new Genre{Games = new List<Game>{new Game{Id = ValidInt}}},
                new Genre{Games = new List<Game>{new Game{Id = ValidInt}}}
            };

            _unitOfUow.Setup(m => m.GenreRepository.Get(null, null)).Returns(_genres);

            var result = _target.GetBy(InvalidInt).ToList().Count;

            Assert.IsTrue(result == 0);
        }
    }
}
