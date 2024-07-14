using AutoMapper.Internal;

using Microsoft.AspNetCore.Mvc;

using PetCareSystem.Services.Models.BookingInfo;

using PetCareSystem.Services.Services.Pay;

using System.Threading.Tasks;



namespace PetCareSystem.WebApp.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    public class PaymentController : ControllerBase

    {

        private IMomoService _momoService;



        public PaymentController(IMomoService momoService)

        {

            _momoService = momoService;

        }



        public IActionResult Index()

        {

            return View();

        }



        [HttpPost]

        public async Task CreatePaymentUrl(BookingInfo model)

        {

            var response = await _momoService.CreatePaymentAsync(model);

            return Redirect(response.PayUrl);

        }



        [HttpGet]

        public IActionResult PaymentCallBack()

        {

            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);

            return View(response);

        }

    }

}