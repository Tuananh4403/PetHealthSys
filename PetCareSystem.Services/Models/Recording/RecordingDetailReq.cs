using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Models.Recording
{
    public class RecordingDetailReq
    {
        public int ServiceId { get; set; }
        public int RecordId { get; set; }
        public int Quantity { get; set; }
    }
}
