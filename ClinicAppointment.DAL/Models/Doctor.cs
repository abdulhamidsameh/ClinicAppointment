namespace ClinicAppointment.DAL.Models;
public class Doctor : BaseEntity
{
    public int ClinicId { get; set; }
    public Clinic Clinic { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public Gender Gender { get; set; }
    public List<DoctorScheduleRule> ScheduleRules { get; set; } = new();
    public List<Appointment> Appointments { get; set; } = new();
}