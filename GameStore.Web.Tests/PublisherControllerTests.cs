﻿using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Web.App_Start;
using GameStore.Web.Controllers;
using GameStore.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Tests
{
    [TestClass]
    public class PublisherControllerTests
    {
        private Mock<IPublisherService> _mockOfPublisherService;
        private PublisherController _target;
        private List<PublisherDto> _publishers;

        [TestInitialize]
        public void Initialize()
        {
            WebAutoMapperConfig.RegisterMappings();
            _publishers = new List<PublisherDto>();
            _mockOfPublisherService = new Mock<IPublisherService>();
            _mockOfPublisherService.Setup(m => m.Create(It.IsAny<PublisherDto>())).Callback<PublisherDto>(p => _publishers.Add(p));
            _mockOfPublisherService.Setup(m => m.GetSingleBy(It.IsAny<string>())).Returns(new PublisherDto());
            _target = new PublisherController(_mockOfPublisherService.Object);
        }

        [TestMethod]
        public void New_SendsPublisherToView()
        {
            var result = ((ViewResult)_target.New()).Model;

            Assert.IsInstanceOfType(result ,typeof(PublisherViewModel));
        }

        [TestMethod]
        public void New_ReturnsHttpStatusCode_WhenModelStateIsValid()
        {
            var result = _target.New(new PublisherViewModel());
            
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void New_SendsPublisherToView_WhenModelStateIsInvalid()
        {
            _target.ModelState.AddModelError("test", "test");
            var result = ((ViewResult)_target.New(new PublisherViewModel())).Model;

            Assert.IsInstanceOfType(result, typeof(PublisherViewModel));
        }

        [TestMethod]
        public void New_CreatesPublisher_WhenModelStateIsValid()
        {
            _target.New(new PublisherViewModel());

            Assert.IsTrue(_publishers.Count == 1);
        }

        [TestMethod]
        public void Show_SendsPublisherToView_WhenAnyCompanyNameIsPassed()
        {
            var result = ((ViewResult)_target.Show(string.Empty)).Model;

            Assert.IsInstanceOfType(result, typeof(PublisherViewModel));
        }
    }
}
