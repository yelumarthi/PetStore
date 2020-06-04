using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetStore.Helper;
using PetStore.Models;
using PetStore.Services;
using PetStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PetStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IPetService _petService;
        private readonly IPetHelper _petHelper;
        public HomeController(IPetService petService, IPetHelper petHelper, ILogger<HomeController> logger)
        {
            _petService = petService;
            _petHelper = petHelper;
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            List<OwnerViewModel> ownerViewModelList = null;
            try
            {
                List<OwnerModel> ownerList = await _petService.GetOwnerDetails();
                ownerViewModelList = _petHelper.GetOwnerViewModelByPetType(ownerList, PetStoreEnums.PetType.Cat);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return View("Error");
            }
            return View(ownerViewModelList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
