namespace ClinicAppointment.PL.ViewModels;

public class ClinicViewModel
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
    [MinLength(5, ErrorMessage = "Min Length of Name is 50 Chars")]
    public string Name { get; set; } = null!;
    public List<Doctor> Doctors { get; set; } = new();
}
