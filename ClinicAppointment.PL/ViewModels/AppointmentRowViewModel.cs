namespace ClinicAppointment.PL.ViewModels;
public class AppointmentRowViewModel
{
    public int Id { get; set; }
    public string PatientName { get; set; } = null!;
    public string DoctorName { get; set; } = null!;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Status { get; set; } = null!;
}
