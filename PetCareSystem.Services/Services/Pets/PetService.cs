﻿using PetCareSystem.Data.Repositories.Pets;
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
using PetCareSystem.Services.Services.Auth;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Models;

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
            int? userId = CommonHelpers.GetUserIdByToken(token);
            if (!userId.HasValue || userId <= 0)
            {
                throw new ArgumentException("Invalid token");
            }

            var customer = await _customerRepository.GetCusByUserId((int)userId);
            if (customer == null)
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
                CustomerId = customer.Id 
            };

            
            await _petRepository.AddAsync(pet);

            
            return true;
        }

        public async Task<PetRequest> GetPetDetailsAsync(int petId)
        {
            // check if id pet exist
            bool exists = await _petRepository.PetExists(petId);
            if (!exists)
            {
                return null;
            }

            var pet = await _petRepository.GetByIdAsync(petId);
            if (pet == null)
            {
                return null;
            }


            var petRequest = new PetRequest
            {
                PetName = pet.PetName,
                KindOfPet = pet.KindOfPet,
                Gender = pet.Gender,
                Birthday = pet.Birthday,
                Species = pet.Species
            };

            return petRequest;
        }

        public async Task<IList<Pet>> GetListPet(string petName, string nameOfCustomer, string kindOfPet, string speciesOfPet, bool? genderOfPet, DateTime? birthdayOfPet)
        {
            return await _petRepository.GetListPet(petName, nameOfCustomer, kindOfPet, speciesOfPet, genderOfPet, birthdayOfPet);
        }

        public async Task<bool> UpdatePetAsync(int id, PetRequest updatePet)
        {
            bool isUpdated = false;
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
            {
                return false;
            }
            else
            {
                pet.PetName = updatePet.PetName;
                pet.KindOfPet = updatePet.KindOfPet;
                pet.Gender = updatePet.Gender;
                pet.Birthday = updatePet.Birthday;
                pet.Species = updatePet.Species;
                isUpdated = await _petRepository.UpdateAsync(pet);
            }

            return isUpdated;
        }

        public async Task<bool> DeletePetAsync(int id)
        {
            Pet pet = await _petRepository.GetByIdAsync(id);
            if (pet != null)
            {
                return await _petRepository.DeleteAsync(pet);
            }
            return false;
        }

        public async Task<ApiResponse<Pet?>> GetPetRecordHis(int petId)
        {
            try
            {
                var record = await _petRepository.GetMedicalHis(petId);
                return new ApiResponse<Pet?>(record, "Get data success");
            }
            catch
            {
                return new ApiResponse<Pet?>(null, message: "Get data fails!");

            }
        }
    }
}
