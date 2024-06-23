using PetCareSystem.Data.Entites;
using PetCareSystem.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;

namespace PetCareSystem.Data.Repositories.Customers
{
    public class CustomerRepository(PetHealthDBContext dbContext, ILogger<CustomerRepository> logger) : BaseRepository<Customer>(dbContext, logger), ICustomerRepository
    {
        public async Task<Customer> GetCusByUserId(int id)
        {
            return await dbContext.Customers.SingleOrDefaultAsync(u => u.UserId == id);
        }
    }
}