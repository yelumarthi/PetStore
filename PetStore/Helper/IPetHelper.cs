using PetStore.Models;
using PetStore.ViewModels;
using System.Collections.Generic;

namespace PetStore.Helper
{
    public interface IPetHelper
    {
        public List<OwnerViewModel> GetOwnerViewModelByPetType(List<OwnerModel> ownerList, PetStoreEnums.PetType petTypeFilter);
    }
}
