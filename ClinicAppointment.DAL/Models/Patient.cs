namespace ClinicAppointment.DAL.Models;
public class Patient : BaseEntity
{
    public string FullName { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Phone { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
}