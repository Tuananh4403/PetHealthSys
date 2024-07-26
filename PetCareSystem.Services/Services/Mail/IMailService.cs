using PetCareSystem.Services.Models;

namespace PetCareSystem.Services.Services.Mail{
    public interface IMailService
    {
        bool SendMail(MailData Mail_Data);
    }
}
