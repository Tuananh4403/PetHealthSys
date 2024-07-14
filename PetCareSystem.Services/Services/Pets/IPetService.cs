using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Models.Pet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Pets
{
    public interface IPetService
    {
        Task<ApiResponse<string>> RegisterPetAsync(PetRequest model, string token);
        Task<PetRequest> GetPetDetailsAsync(int petId);
        Task<PaginatedApiResponse<Object>> GetListPet(string? petName, string? nameOfCustomer, bool? saveBarn, int pageNumber = 1, int pageSize = 10);
        Task<bool> UpdatePetAsync(int id, PetRequest updatePet);
        Task<bool> DeletePetAsync(int id);
        Task<ApiResponse<Pet?>> GetPetRecordHis(int petId);
        Task<PaginatedApiResponse<Pet>> GetListPetByUserId(string token, bool? saveBarn,int pageNumber = 1, int pageSize = 10);
    }
}
