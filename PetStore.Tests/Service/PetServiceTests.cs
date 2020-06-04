using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using PetStore.Models;
using PetStore.Services;
using PetStore.Tests.MockHttpHandler;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace PetStore.Tests.Service
{
    public class PetServiceTests
    {

        [Fact]
        public async void GetOwnerDetails_NotNull()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockLogger = new Mock<ILogger<PetService>>();
            var mockConfig = new Mock<IConfiguration>();

            mockFactory.Setup(mock => mock.CreateClient(It.IsAny<string>())).Returns(new HttpClient());
            var service = new PetService(mockFactory.Object, mockLogger.Object, mockConfig.Object);

            List<OwnerModel> serviceResponse = new List<OwnerModel>();
            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, serviceResponse);
                return Task.FromResult(response);
            });

            var result = await service.GetOwnerDetails();
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetOwnerDetails_ExceptionInApiCall()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockLogger = new Mock<ILogger<PetService>>();
            var mockConfig = new Mock<IConfiguration>();

            mockFactory.Setup(mock => mock.CreateClient(It.IsAny<string>())).Throws(new Exception());
            var service = new PetService(mockFactory.Object, mockLogger.Object, mockConfig.Object);

            List<OwnerModel> serviceResponse = new List<OwnerModel>();
            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, serviceResponse);
                return Task.FromResult(response);
            });

            Task act() => service.GetOwnerDetails(); 
            await Assert.ThrowsAsync<Exception>(act);
        }
    }
}
