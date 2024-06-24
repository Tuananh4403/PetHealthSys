namespace PetCareSystem.Data.Enums
{
    public enum BookingStatus
    {
        Review,    // The booking is created but not yet confirmed
        Confirmed,  // The booking is confirmed
        Cancelled,  // The booking is cancelled
        Completed   // The service related to the booking is completed
    }
}