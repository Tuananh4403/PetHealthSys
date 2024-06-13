using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Repositories.Bookings;
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
        
        public BookingServices(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> CreateBookingAsync(CreateBookingReq bookingReq)
        {

            var booking = new Booking ()
            {
                CustomerId = bookingReq.CustomerId,
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

        public async Task<bool> UpdateBookingAsync(int BookingId, CreateBookingReq updateReq)
        {
            var bookingToUpdate = await _bookingRepository.SaveChangesAsync();
            if (bookingToUpdate == null)
            {
                    return false;
                }

            if(await _bookingRepository.DeleteBookingAsync(BookingId))
            {
                CreateBookingAsync(updateReq);
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
