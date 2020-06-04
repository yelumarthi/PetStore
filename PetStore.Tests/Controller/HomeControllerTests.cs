using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PetStore.Controllers;
using PetStore.Helper;
using PetStore.Models;
using PetStore.Services;
using PetStore.ViewModels;
using System;
using System.Collections.Generic;
using Xunit;

namespace PetStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async void Index_Return_Correct_Data()
        {
            var serviceResponse = new List<OwnerViewModel>() {
                new OwnerViewModel(){
                    Gender = "Male",
                    PetNames = new List<string> { "Garfield", "Jim", "Max", "Tom" }
                },
                new OwnerViewModel(){
                    Gender = "Female",
                    PetNames = new List<string> { "Garfield", "Simba", "Tabby" }
                }
            };

            var mockPetService = new Mock<IPetService>();
            var mockPetHelper = new Mock<IPetHelper>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockPetService.Object, mockPetHelper.Object, mockLogger.Object);

            mockPetHelper.Setup(mock => mock.GetOwnerViewModelByPetType(It.IsAny<List<OwnerModel>>(), PetStoreEnums.PetType.Cat))
                        .Returns(serviceResponse);

            var result = await controller.Index();
            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult.Model);
            var viewModel = viewResult.Model as List<OwnerViewModel>;
            Assert.Equal(serviceResponse, viewModel);
        }

        [Fact]
        public async void Index_Exception_Returns_Error_View()
        {
            var mockPetService = new Mock<IPetService>();
            var mockPetHelper = new Mock<IPetHelper>();
            var mockLoggerService = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockPetService.Object, mockPetHelper.Object, mockLoggerService.Object);

            mockPetHelper.Setup(mock => mock.GetOwnerViewModelByPetType(It.IsAny<List<OwnerModel>>(), PetStoreEnums.PetType.Cat))
                .Throws(new Exception());
            var result = await controller.Index();
            var viewResult = result as ViewResult;
            Assert.Equal("Error", viewResult.ViewName);
        }
    }
}
