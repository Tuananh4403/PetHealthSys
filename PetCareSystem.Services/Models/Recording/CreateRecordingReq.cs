﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Services.Models.Recording
{
    public class CreateRecordingReq
    {
        public int PetId { get; set; }
        public int[]? ServiceIds { get; set; }
        public int? BarnId { get; set; }
        public string? DetailPrediction { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string? Conclude { get; set; }
        public bool SaveBarn { get; set; }
        public Dictionary<int, int>? ServiceQuantities { get; set; }
    }
}
