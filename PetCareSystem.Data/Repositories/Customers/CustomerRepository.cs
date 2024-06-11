using PetCareSystem.Data.Entites;
using PetCareSystem.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PetCareSystem.Data.Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PetHealthDBContext _dbContext;

        public CustomerRepository(PetHealthDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCustomerAsync(Customer cus)
        {
            await _dbContext.Customers.AddAsync(cus);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetCusById(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}