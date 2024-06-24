using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Enums;
using PetCareSystem.Data.Repositories.Bookings;
using PetCareSystem.Data.Repositories.BookingServices;
using PetCareSystem.Data.Repositories.Customers;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Bookings
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBookingServiceRepository _bookingServiceRepository;
        
        public BookingServices(IBookingRepository bookingRepository, ICustomerRepository customerRepository, IBookingServiceRepository bookingService)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _bookingServiceRepository = bookingService;
        }

        public async Task<bool> CreateBookingAsync(CreateBookingReq bookingReq, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            if (!userId.HasValue || userId <= 0)
            {
                throw new ArgumentException("Invalid token");
            }

            var customer = await _customerRepository.GetCusByUserId((int)userId);
            var booking = new Booking ()
            {
                CustomerId = customer.Id,
                PetId = bookingReq.PetId,
                BookingTime = bookingReq.BookingDate,
                Status      = BookingStatus.Review
                // Set other properties of the Booking entity as needed
            };

            // Save the booking entity to the database
            if (!await _bookingRepository.AddAsync(booking))
            {
                return false;
            }

            // Create related BookingService entities
            foreach (var serviceId in bookingReq.ServiceIds)
            {
                var bookingService = new BookingService
                {
                    BookingId = booking.Id,
                    ServiceId = serviceId,
                    // Set other properties of the BookingService entity as needed
                };

                // Save each BookingService entity to the database
                if (!await _bookingServiceRepository.AddAsync(bookingService))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> UpdateBookingAsync(int BookingId, CreateBookingReq updateReq, string token)
        {
            var bookingToUpdate = await _bookingRepository.GetByIdAsync(BookingId);
            if (bookingToUpdate == null)
            {
                    return false;
            }
            bookingToUpdate.BookingTime = updateReq.BookingDate;
            //bookingToUpdate.

            return true;
        }


        //public async Task<Booking> GetBookingById(int BookingId)
        //{
        //    return (Booking)await _bookingRepository.GetListBooking(BookingId);
        //}

        public async Task<bool> DeleteBooking(int BookingId)
        {
            Booking booking = await _bookingRepository.GetByIdAsync(BookingId);
            if(booking  != null)
            {
                return await _bookingRepository.DeleteAsync(booking);
            }
            return true;
        }

        Task<Booking> IBookingServices.GetBookingById(int BookingId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<string>> ConfirmBooking(int bookingId)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(bookingId);
                if (booking == null)
                {
                    return new ApiResponse<string>("Booking not exist! ", true);
                }else if(booking.Status == BookingStatus.Review)
                {
                    return new ApiResponse<string>("Booking have been confirmed! ", true);

                }
                booking.Status = BookingStatus.Confirmed;
                _bookingRepository.UpdateAsync(booking);
                return new ApiResponse<string>("Booking Confirm successfully");
            }
            catch (Exception ex) 
            {
                return new ApiResponse<string>("Error system", true);
            }
           
        }
    }
}
