namespace ClinicAppointment.PL.ViewModels;

public class PatientViewModel
{
    [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
    [MinLength(5, ErrorMessage = "Min Length of Name is 50 Chars")]
    public string FullName { get; set; } = null!;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    [Phone]
    [DataType(DataType.PhoneNumber)]
    public string? Phone { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
}
