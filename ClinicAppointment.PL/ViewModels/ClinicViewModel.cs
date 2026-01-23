namespace ClinicAppointment.PL.ViewModels;

public class ClinicViewModel
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; } = null!;
    public List<Doctor> Doctors { get; set; } = new();
}
