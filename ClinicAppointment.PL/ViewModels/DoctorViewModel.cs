namespace ClinicAppointment.PL.ViewModels;
public class DoctorViewModel
{
    public int Id { get; set; }
    public int ClinicId { get; set; }
    public Clinic? Clinic { get; set; } = null!;
    [MaxLength(50,ErrorMessage ="Max Length of Name is 50 Chars")]
    [MinLength(5,ErrorMessage ="Min Length of Name is 50 Chars")]
    public string FullName { get; set; } = null!;
    public Gender Gender { get; set; }
    public List<DoctorScheduleRule> ScheduleRules { get; set; } = new();
    public List<Appointment> Appointments { get; set; } = new();
    public bool IsDeleted { get; set; }
}
