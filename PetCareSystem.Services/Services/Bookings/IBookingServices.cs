﻿using PetCareSystem.Data.Entites;
using PetCareSystem.Data.Enums;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Bookings
{
    public interface IBookingServices
    {
        Task<ApiResponse<string>> CreateBookingAsync(CreateBookingReq bookingReq, string token);
        Task<bool> UpdateBookingAsync(int BookingId, CreateBookingReq updateReq, string token);
        Task<PaginatedApiResponse<Booking>> GetListBookingAsync(DateTime? bookingDate = null, string token = "", BookingStatus status = BookingStatus.Review, int pageNumber = 1, int pageSize = 10);
        Task<Booking> GetBookingById(int BookingId);
        Task<bool> DeleteBooking(int BookingId);
        Task<ApiResponse<string>> ConfirmBooking(int bookingId, string token);
        Task<ApiResponse<string>> Cancel(int id, string token);
    }
}
