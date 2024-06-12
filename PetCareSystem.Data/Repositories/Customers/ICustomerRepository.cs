using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Customers
{
    public interface ICustomerRepository
    {
       
        Task AddCustomerAsync(Customer cus);
        Task<User> GetCusById(int id);
        
        int? GetUserIdFromToken(string token);
    }
}