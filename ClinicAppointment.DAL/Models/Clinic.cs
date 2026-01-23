using System.ComponentModel.DataAnnotations;
namespace ClinicAppointment.DAL.Models;
public class Clinic : BaseEntity
{
    [MaxLength(50, ErrorMessage = "Max Length of Name is 50 Chars")]
    [MinLength(5, ErrorMessage = "Min Length of Name is 50 Chars")]
    public string Name { get; set; } = null!;
    public List<Doctor> Doctors { get; set; } = new();
}