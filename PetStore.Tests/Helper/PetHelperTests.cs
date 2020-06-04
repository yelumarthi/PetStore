using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using PetStore.Helper;
using PetStore.Models;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PetStore.Tests.Helper
{
    public class PetHelperTests
    {
        private List<OwnerModel> MockOwnerData()
        {
            List<OwnerModel> ownerList = new List<OwnerModel>();
            using (StreamReader r = new StreamReader("Helper/file.json"))
            {
                string json = r.ReadToEnd();
                ownerList = JsonConvert.DeserializeObject<List<OwnerModel>>(json);
            }
            return ownerList;
        }

        [Fact]
        public void GetOwnerViewModelByPetType_Returning_Correct_Value()
        {
            var ownerList = MockOwnerData();
            var mockLogger = new Mock<ILogger<PetHelper>>();
            var helper = new PetHelper(mockLogger.Object);
            var ownerViewModel = helper.GetOwnerViewModelByPetType(ownerList, PetStoreEnums.PetType.Cat);

            Assert.NotNull(ownerViewModel);
            Assert.Equal(2, ownerViewModel.Count);

            Assert.Equal("Male", ownerViewModel[0].Gender);
            Assert.NotNull(ownerViewModel[0].PetNames);
            Assert.Equal(4, ownerViewModel[0].PetNames.Count);
            Assert.Equal("Garfield", ownerViewModel[0].PetNames[0]);
            Assert.Equal("Jim", ownerViewModel[0].PetNames[1]);
            Assert.Equal("Max", ownerViewModel[0].PetNames[2]);
            Assert.Equal("Tom", ownerViewModel[0].PetNames[3]);

            Assert.Equal("Female", ownerViewModel[1].Gender);
            Assert.NotNull(ownerViewModel[1].PetNames);
            Assert.Equal(3, ownerViewModel[1].PetNames.Count);
            Assert.Equal("Garfield", ownerViewModel[1].PetNames[0]);
            Assert.Equal("Simba", ownerViewModel[1].PetNames[1]);
            Assert.Equal("Tabby", ownerViewModel[1].PetNames[2]);

        }

        [Fact]
        public void GetOwnerViewModelByPetType_Returns_empty_viewModel()
        {
            List<OwnerModel> ownerList = null;
            var mockLogger = new Mock<ILogger<PetHelper>>();
            var helper = new PetHelper(mockLogger.Object);
            var ownerViewModel = helper.GetOwnerViewModelByPetType(ownerList, PetStoreEnums.PetType.Cat);

            Assert.NotNull(ownerViewModel);
            Assert.Empty(ownerViewModel);
        }

    }
}
