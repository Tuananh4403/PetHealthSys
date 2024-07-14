using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCareSystem.Services.Models.Momo
{
    public class RecordPaymentRequestModel
    {
        public string RecordId { get; set; }
        public double Amount { get; set; }  // Tổng số tiền thanh toán cho Record
        public string DetailPrediction { get; set; }  // Thông tin chi tiết cho Record
    }
}
