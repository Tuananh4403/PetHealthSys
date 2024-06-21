using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Bookings;
using PetCareSystem.Data.Repositories.Customers;
using PetCareSystem.Services.Helpers;
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
        
        public BookingServices(IBookingRepository bookingRepository, ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateBookingAsync(CreateBookingReq bookingReq, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            if (!userId.HasValue || userId <= 0)
            {
                throw new ArgumentException("Invalid token");
            }

            var customer = await _customerRepository.GetCusById((int)userId);
            var booking = new Booking ()
            {
                CustomerId = customer.Id,
                PetId = bookingReq.PetId,
                BookingTime = bookingReq.BookingDate,
                // Set other properties of the Booking entity as needed
            };

            // Save the booking entity to the database
            if (!await _bookingRepository.CreateBookingAsync(booking))
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
                if (!await _bookingRepository.AddBookingServiceAsync(bookingService))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> UpdateBookingAsync(int BookingId, CreateBookingReq updateReq, string token)
        {
            var bookingToUpdate = await _bookingRepository.SaveChangesAsync();
            if (bookingToUpdate == null)
            {
                    return false;
                }

            if(await _bookingRepository.DeleteBookingAsync(BookingId))
            {
                CreateBookingAsync(updateReq, token);
            }
            else
            {
                return false;
            }

            return true;
        }


        //public async Task<Booking> GetBookingById(int BookingId)
        //{
        //    return (Booking)await _bookingRepository.GetListBooking(BookingId);
        //}

        public async Task<bool> DeleteBooking(int BookingId)
        {
            if(!(await _bookingRepository.DeleteBookingAsync(BookingId)))
            {
                return false;
            }

                return true;
        }

        Task<Booking> IBookingServices.GetBookingById(int BookingId)
        {
            throw new NotImplementedException();
        }
    }
}
