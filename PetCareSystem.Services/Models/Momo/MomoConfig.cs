﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Models.Momo
{
    public class MomoConfig
    {
        public string MomoApiUrl { get; set; }
        public string SecretKey { get; set; }
        public string AccessKey { get; set; }
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public string PartnerCode { get; set; }
        public string RequestType { get; set; }
    }

}
