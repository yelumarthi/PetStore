using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetStore.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public class PetService : IPetService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private ILogger _logger;
        private IConfiguration _config;
        public PetService(IHttpClientFactory clientFactory, ILogger<PetService> logger, IConfiguration config)
        {
            _httpClientFactory = clientFactory;
            _logger = logger;
            _config = config;
        }

        public async Task<List<OwnerModel>> GetOwnerDetails()
        {
            var serviceUrl = _config["AGL_DevTest_ApiUrl"];
            List<OwnerModel> ownerList = new List<OwnerModel>();
            var client = _httpClientFactory.CreateClient();
            if (!string.IsNullOrEmpty(serviceUrl))
            {
                try
                {

                    var request = new HttpRequestMessage(HttpMethod.Get, serviceUrl);
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        ownerList = await response.Content.ReadAsAsync<List<OwnerModel>>();
                    }
                    else
                    {
                        _logger.LogError("Error in GetOwnerDetails - API call");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error in GetOwnerDetails " + ex.Message);
                    throw ex;
                }
            }

            return ownerList;
        }
    }
}
