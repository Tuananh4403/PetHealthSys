﻿using PetCareSystem.Data.Entites;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Services.Models.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Records
{
    public interface IRecordServices
    {
        Task<ApiResponse<string>> CreateRecordAsync(CreateRecordingReq createRecordReq, string token);
        Task<ApiResponse<Record?>> GetRecordHis(int petId);
        Task<ApiResponse<string>> CreateRecordByBookingAsync(int bookingId, string token);
    }
}
