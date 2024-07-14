using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

namespace PetCareSystem.Services.Models.MomoResult
{
    public class MomoResult
    {
        public string? partnerCode { get; set; }
        public int? bookingId { get; set; }
        public int? requestId { get; set; }
        public decimal amount { get; set; }
        public string? bookingInfo { get; set; }
        public string? bookingType { get; set; }
        public string? transId { get; set; }
        public int resultCode { get; set; }
        public string? payType { get; set; }

        public string? message { get; set; }
        public long responseTime { get; set; }
        public string? extraData { get; set; }
        public string? signature { get; set; }

        public bool IsValidSignature(string accessKey, string secret)
        {
            var rawHash = "accessKey=" + accessKey +
                        "&amount=" + this.amount +
                        "&extraData=" + this.extraData +
                        "&message=" + this.message +
                        "&bookingId=" + this.bookingId +
                        "&bookingInfo=" + this.bookingInfo +
                        "&bookingType=" + this.bookingType +
                        "&partnerCode=" + this.partnerCode +
                        "&payType=" + this.payType +
                        "&requestId=" + this.requestId +
                        "&responseTime=" + this.responseTime +
                        "&result=" + this.resultCode +
                        "transId=" + this.transId;
            var checkSignature = HashHelper
        }
    }
}
