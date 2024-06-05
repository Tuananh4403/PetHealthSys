using Microsoft.AspNetCore.Mvc;

namespace PetCareSystem.WebApp.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
