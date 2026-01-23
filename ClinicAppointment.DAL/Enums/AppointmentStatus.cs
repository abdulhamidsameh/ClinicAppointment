namespace ClinicAppointment.DAL.Enums;
public enum AppointmentStatus
{
    [EnumMember(Value = "Unknown")]
    Unknown = 0,
    [EnumMember(Value = "Booked")]
    Booked = 1,
    [EnumMember(Value = "Completed")]
    Completed = 2,
    [EnumMember(Value = "Cancelled")]
    Cancelled = 3
}