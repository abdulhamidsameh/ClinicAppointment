namespace ClinicAppointment.PL.ViewModels;
public class CreateAppointmentViewModel
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public DateTime? SelectedStartDateTime { get; set; }

    public int VisitMinutes { get; set; } = 30;

    public List<SelectListItem> Patients { get; set; } = new();
    public List<SelectListItem> Doctors { get; set; } = new();
}
