using PetStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public interface IPetService
    {
        Task<List<OwnerModel>> GetOwnerDetails();
    }
}
