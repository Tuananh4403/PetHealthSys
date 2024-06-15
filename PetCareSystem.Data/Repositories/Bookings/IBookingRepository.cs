﻿using PetCareSystem.Data.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Data.Repositories.Bookings
{
    public interface IBookingRepository
    {
        Task<Boolean> CreateBookingAsync(Booking booking);
        Task<Boolean> AddBookingServiceAsync(BookingService bookingService);
        Task<Boolean> SaveChangesAsync();
        Task<Booking> GetListBooking(int bookingId);
        Task<IList<Booking>> GetListBooking(string name);
        Task<bool> CancelBooking(int bookingId);
        
        Task<bool> UpdateBooking(int bookingId, DateTime bookingTime, int[] serviceIds);
    }
}