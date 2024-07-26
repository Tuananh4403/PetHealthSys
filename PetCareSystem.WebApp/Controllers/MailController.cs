using Microsoft.AspNetCore.Mvc;
using PetCareSystem.Services.Models;
using PetCareSystem.Services.Services.Mail;
using PetCareSystem.WebApp.Helpers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MailController : ControllerBase
{
    IMailService Mail_Service = null;
//injecting the IMailService into the constructor
    public MailController(IMailService _MailService)
    {
         Mail_Service = _MailService;
    }
    [HttpPost]
    public bool SendMail(MailData Mail_Data)
    {
        return Mail_Service.SendMail(Mail_Data);
    }
}