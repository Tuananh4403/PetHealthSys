using System.Globalization;
namespace PetHealthSys.PetCareSystem.WebApp.Helpers
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }
        public AppException(string message, params object[] ars) : base(String.Format(CultureInfo.CurrentCulture, message, ars))
        {

        }
    }
}
