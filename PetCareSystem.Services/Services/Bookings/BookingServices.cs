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
using System.Globalization;
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

        public async Task<ApiResponse<string>> CreateBookingAsync(CreateBookingReq bookingReq, string token)
        {
            int? userId = CommonHelpers.GetUserIdByToken(token);
            string message = "";
            bool result = false;
            if (!userId.HasValue || userId <= 0)
            {
                throw new ArgumentException("Invalid token");
            }

            var customer = await _customerRepository.GetCusByUserId((int)userId);
            var booking = new Booking()
            {
                CustomerId = customer.Id,
                DoctorId = bookingReq.DoctorId != 0 ? bookingReq.DoctorId : null,
                PetId = bookingReq.PetId,
                BookingTime = DateTime.ParseExact(bookingReq.BookingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Shift = GetBookingShiftById(bookingReq.Shift),
                Status = BookingStatus.Review
                // Set other properties of the Booking entity as needed
            };
            // Save the booking entity to the database
            (result, message) = await _bookingRepository.CheckReviewBooking(booking);
            if (result)
            {
                if (await _bookingRepository.AddAsync(booking))
                {
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
                            result = false;
                            message = "Create booking detail fails!";
                        }
                    }
                }else{ 
                    result = false;
                    message = "Create booking fails!";
                }
            }


            // Create related BookingService entities


            return new ApiResponse<string>(message, !result);
        }

        public async Task<bool> UpdateBookingAsync(int BookingId, CreateBookingReq updateReq, string token)
        {
            var bookingToUpdate = await _bookingRepository.GetByIdAsync(BookingId);
            if (bookingToUpdate == null)
            {
                return false;
            }
            bookingToUpdate.BookingTime = DateTime.ParseExact(updateReq.BookingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
            if (booking != null)
            {
                return await _bookingRepository.DeleteAsync(booking);
            }
            return true;
        }

        async Task<Booking> IBookingServices.GetBookingById(int BookingId)
        {
            return await _bookingRepository.GetByIdAsync(BookingId);
        }

        public async Task<ApiResponse<string>> ConfirmBooking(int bookingId)
        {
            try
            {
                Booking booking = await _bookingRepository.GetByIdAsync(bookingId);
                if (booking == null)
                {
                    return new ApiResponse<string>("Booking not exist! ", true);
                }
                else if (booking.Status != BookingStatus.Review)
                {
                    return new ApiResponse<string>("Booking status not in Review! ", true);

                }
                var (checkBooking, message) = await _bookingRepository.CheckReviewBooking(booking);
                if (checkBooking)
                {
                    booking.Status = BookingStatus.Confirmed;
                    bool result = await _bookingRepository.UpdateAsync(booking);
                    if (result)
                    {
                        return new ApiResponse<string>(message: "Booking have been confirmed!", false);
                    }
                }
                else
                {
                    return new ApiResponse<string>(message, true);
                }
                return new ApiResponse<string>("Booking Confirm successfully");
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>("Error system", true);
            }
        }
        public async Task<PaginatedApiResponse<Booking>> GetListBookingAsync(DateTime? bookingDate = null, string token = "", BookingStatus status = BookingStatus.Review, int pageNumber = 1, int pageSize = 10)
        {
            var user = CommonHelpers.GetUserIdByToken(token);
            var cus = await _customerRepository.GetCusByUserId((int)user);
            var (booking, totalCount) = await _bookingRepository.GetListBooking(bookingDate, status, pageNumber, pageSize, cus);
            if (!booking.Any())
            {
                return new PaginatedApiResponse<Booking>("No Booking found", true);
            }
            return new PaginatedApiResponse<Booking>(booking, totalCount, pageNumber, pageSize);
        }
        public static BookingShift GetBookingShiftById(int id)
        {
            if (Enum.IsDefined(typeof(BookingShift), id))
            {
                return (BookingShift)id;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid booking shift ID.");
            }
        }
    }
}
