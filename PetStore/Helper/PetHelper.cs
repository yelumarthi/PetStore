using Microsoft.Extensions.Logging;
using PetStore.Models;
using PetStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetStore.Helper
{
    public class PetHelper : IPetHelper
    {
        private ILogger _logger;
        public PetHelper(ILogger<PetHelper> logger)
        {
            _logger = logger;
        }

        public List<OwnerViewModel> GetOwnerViewModelByPetType(List<OwnerModel> ownerList, PetStoreEnums.PetType petTypeFilter)
        {
            var ownerViewModelList = new List<OwnerViewModel>();
            try
            {       
                if (ownerList != null && ownerList.Any())
                {
                    ownerViewModelList = ownerList
                                               .Where(o => o.Pets != null && o.Pets.Any(p => p.Type.ToLower() == petTypeFilter.ToString().ToLower()))
                                               .Select(o => new
                                               {
                                                   o.Gender,
                                                   Pets = o.Pets.Where(p => p.Type.ToLower() == petTypeFilter.ToString().ToLower())
                                               })
                                               .GroupBy(x => x.Gender,
                                                        x => x.Pets,
                                                        (key, group) => new
                                                        {
                                                            Gender = key,
                                                            Pets = group.SelectMany(p => p)
                                                        })
                                               .OrderByDescending(x => x.Gender)
                                               .Select(x => new OwnerViewModel
                                               {
                                                   Gender = x.Gender,
                                                   PetNames = x.Pets
                                                            .OrderBy(p => p.Name)
                                                            .Select(p => p.Name)
                                                            .ToList()
                                               })
                                               .ToList();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error in GetOwnerViewModelByCat " + ex.Message);
                throw ex;
            }
            return ownerViewModelList;
        }
    }
}
