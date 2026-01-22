namespace ClinicAppointment.DAL.Models;
public class Clinic : BaseEntity
{
    public string Name { get; set; } = null!;
    public List<Doctor> Doctors { get; set; } = new();
}