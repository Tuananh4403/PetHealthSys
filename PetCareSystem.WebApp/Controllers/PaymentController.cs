﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetCareSystem.Services.Models.Momo;
using PetCareSystem.Services;
using System.Threading.Tasks;
using PetCareSystem.Services.Services.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PetCareSystem.Services.Helpers;
using PetCareSystem.Services.Services.Momo;
using Microsoft.AspNetCore.Http;

namespace PetCareSystem.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IMomoPaymentService _momoService;

        public PaymentController(MomoPaymentService momoService)
        {
            _momoService = momoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentUrl(OrderInfoModel model)
        {
            var payUrl = await _momoService.CreatePaymentAsync(model);

            return Redirect(payUrl);
        }

        [HttpGet("get")]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return Ok(response);
        }
    }
}
