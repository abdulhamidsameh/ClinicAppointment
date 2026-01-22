namespace ClinicAppointment.DAL.Models;
public class Appointment : BaseEntity
{
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public int VisitMinutes { get; set; }
    public AppointmentStatus Status { get; set; }
}