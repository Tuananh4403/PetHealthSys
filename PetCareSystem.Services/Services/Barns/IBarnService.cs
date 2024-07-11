using PetCareSystem.Services.Models.Barn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Barns
{
    public interface IBarnService
    {
        Task<bool> CreateBarnsAsync(BarnRequest barnRequest, string token);

        Task<bool> UpdateBarnsAsync(int barnId, BarnRequest barnRequest, string token);
    }
}
