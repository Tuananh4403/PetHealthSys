﻿using System.ComponentModel.DataAnnotations;

namespace PetCareSystem.Services.Models.Booking
{
    public class CreateBookingReq
    {
        public int CustomerId { get; set; }
        public int PetId { get; set; }
        public int DoctorId { get; set; }
        public int[]? ServiceIds { get; set; }
        public int Shift { get; set; }
        public string? BookingDate { get; set;}
        public string? Note { get; set;}
    }
}
