using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Customers
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCusByUserId(int id);
    }
}