using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PetCareSystem.Services.Models.Momo;
using PetCareSystem.Services.Services.Momo;
using RestSharp;
using Microsoft.AspNetCore.Http;
using QRCoder;
public class MomoPaymentService : IMomoPaymentService
{
    private readonly IOptions<MomoConfig> _options;

    public MomoPaymentService(IOptions<MomoConfig> options)
    {
        _options = options;
    }

    private string ComputeHmacSha256(string message, string secretKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        var messageBytes = Encoding.UTF8.GetBytes(message);

        using (var hmac = new HMACSHA256(keyBytes))
        {
            var hashBytes = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public async Task<string> CreatePaymentAsync(OrderInfoModel model)
    {
        model.OrderId = DateTime.UtcNow.Ticks.ToString();
        model.OrderInfo = "Customer: " + model.FullName + ". Content: " + model.OrderInfo;

        var rawData = $"partnerCode={_options.Value.PartnerCode}&accessKey={_options.Value.AccessKey}&requestId={model.OrderId}&amount={model.Amount}&orderId={model.OrderId}&orderInfo={model.OrderInfo}&returnUrl={_options.Value.ReturnUrl}&notifyUrl={_options.Value.NotifyUrl}&extraData=";
        var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

        var client = new RestClient(_options.Value.MomoApiUrl);
        var request = new RestRequest
        {
            Method = Method.Post,  
            RequestFormat = DataFormat.Json
        };
        request.AddHeader("Content-Type", "application/json; charset=UTF-8");

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
            ipnUrl = "",
            extraData = "",
            lang = "",
            signature = signature
        };

        request.AddJsonBody(requestData);
        var response = await client.ExecuteAsync(request);

        var responseContent = JsonConvert.DeserializeObject<MomoResponse>(response.Content);
        return responseContent?.PayUrl;
    }

    public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection)
    {
        var amount = collection["amount"];
        var orderInfo = collection["orderInfo"];
        var orderId = collection["orderId"];

        return new MomoExecuteResponseModel
        {
            Amount = amount,
            OrderId = orderId,
            OrderInfo = orderInfo
        };
    }
}
