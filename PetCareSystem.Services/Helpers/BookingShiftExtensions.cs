using PetCareSystem.Data.Enums;

public static class BookingShiftExtensions
{
    public static string GetTimeRange(this BookingShift shift)
    {
        return shift switch
        {
            BookingShift.Morning_8AM_10AM => "8:00 AM - 10:00 AM",
            BookingShift.LateMorning_10AM_12PM => "10:00 AM - 12:00 PM",
            BookingShift.Afternoon_1PM_3PM => "1:00 PM - 3:00 PM",
            BookingShift.LateAfternoon_3PM_5PM => "3:00 PM - 5:00 PM",
            _ => "Unknown"
        };
    }
    public static List<BookingShiftDto> GetAllBookingShifts()
{
    return Enum.GetValues(typeof(BookingShift))
               .Cast<BookingShift>()
               .Select(shift => new BookingShiftDto
               {
                   Id = (int)shift,
                   Shift = shift.ToString(),
                   TimeRange = shift.GetTimeRange()
               })
               .ToList();
}
}