using PetCareSystem.Data.Repositories.Pets;
using PetCareSystem.Data.Repositories.Users;
using PetCareSystem.Services.Models.Pet;
using PetCareSystem.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using PetCareSystem.Data.Repositories.Customers;
using Microsoft.IdentityModel.Tokens;

namespace PetCareSystem.Services.Services.Pets
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly ICustomerRepository _customerRepository;

        public PetService(IPetRepository petRepository, ICustomerRepository customerRepository)
        {
            _petRepository = petRepository;
            _customerRepository = customerRepository;
        }
        public async Task<bool> RegisterPetAsync(PetRequest model, string token)
        {
            var userId = _customerRepository.GetUserIdFromToken(token);
            if (!userId.HasValue || userId <= 0)
            {
                throw new ArgumentException("Invalid token");
            }

            var user = await _customerRepository.GetCusById((int)userId);
            if (user == null)
            {
                throw new ArgumentException("Customer does not exist! You need to register");
            }

           
            if (string.IsNullOrEmpty(model.PetName))
            {
                throw new ArgumentException("Name of Pet cannot be blank");
            }
            if (string.IsNullOrEmpty(model.KindOfPet))
            {
                throw new ArgumentException("Kind of Pet cannot be blank");
            }
            if (string.IsNullOrEmpty(model.Species))
            {
                throw new ArgumentException("Species cannot be blank");
            }
            if (model.Birthday == DateTime.MinValue)
            {
                throw new ArgumentException("Birthday is invalid value");
            }
            if (model.Birthday > DateTime.Now)
            {
                throw new ArgumentException("Birthday cannot be in the future");
            }

            
            var pet = new Pet
            {
                PetName = model.PetName,
                KindOfPet = model.KindOfPet,
                Gender = model.Gender,
                Birthday = model.Birthday,
                Species = model.Species,
                CustomerId = user.Id 
            };

            
            await _petRepository.AddPetAsync(pet);

            
            return true;
        }
    }
}
