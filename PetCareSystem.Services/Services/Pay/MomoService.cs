using Newtonsoft.Json;

using RestSharp;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using PetCareSystem.Services.Models.BookingInfo;

using PetCareSystem.Services.Models.MomoResponse;



namespace PetCareSystem.Services.Services.Pay

{

    public class MomoService : IMomoService

    {

        public async Task<MomoResponse> CreatePaymentAsync(BookingInfo model)

        {

            model.Id = await

            var rawData =

                $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={model.OrderId}&amount={model.Amount}&orderId={model.OrderId}&orderInfo={model.OrderInfo}&returnUrl={_options.Value.ReturnUrl}¬ifyUrl={_options.Value.NotifyUrl}&extraData=";



            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);



            var client = new RestClient(_options.Value.MomoApiUrl);

            var request = new RestRequest() { Method = Method.Post };

            request.AddHeader("Content-Type", "application/json; charset=UTF-8");



            // Create an object representing the request data 

            var requestData = new

            {

                accessKey = _options.Value.AccessKey,

                partnerCode = _options.Value.PartnerCode,

                requestType = _options.Value.RequestType,

                notifyUrl = _options.Value.NotifyUrl,

                returnUrl = _options.Value.ReturnUrl,

                orderId = model.OrderId,

                amount = model.Amount.ToString(),

                orderInfo = model.OrderInfo,

                requestId = model.OrderId,

                extraData = "",

                signature = signature

            };



            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);



            var response = await client.ExecuteAsync(request);



            return JsonConvert.DeserializeObject(response.Content);

        }



    }

}