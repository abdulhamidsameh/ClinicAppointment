namespace ClinicAppointment.PL.ViewModels;

public class DoctorScheduleRuleViewModel
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; } = null!;
    public DayOfWeek DayOfWeek { get; set; }
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan StartTime { get; set; }
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    public TimeSpan EndTime { get; set; }
    public bool IsOff { get; set; }
}
