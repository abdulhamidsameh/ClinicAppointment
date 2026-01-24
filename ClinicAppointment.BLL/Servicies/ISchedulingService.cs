namespace ClinicAppointment.BLL.Servicies;
public interface ISchedulingService
{
    Task<List<DateTime>> GetFreeSlotsAsync(int doctorId, DateOnly date, int visitMinutes = 30);
}