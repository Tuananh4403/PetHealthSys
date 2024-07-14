using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using PetCareSystem.Services.Models.BookingInfo;

using PetCareSystem.Services.Models.Momo;



namespace PetCareSystem.Services.Services.Pay

{

    public interface IMomoService

    {

        Task<Momo> CreatePaymentAsync(BookingInfo model);

    }

}