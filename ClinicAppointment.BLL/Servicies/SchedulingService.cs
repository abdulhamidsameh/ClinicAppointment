namespace ClinicAppointment.BLL.Servicies;
public class SchedulingService : ISchedulingService
{
    private readonly IUnitOfWork _unitOfWork;

    public SchedulingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<DateTime>> GetFreeSlotsAsync(int doctorId, DateOnly date, int visitMinutes = 30)
    {
        var day = date.DayOfWeek;
        var ruleSpec = new BaseSpecifications<DoctorScheduleRule>(r => r.DoctorId == doctorId && r.DayOfWeek == day);
        var rule = await _unitOfWork.Repository<DoctorScheduleRule>().GetWithSpecAsync(ruleSpec);

        if (rule is null || rule.IsOff)
            return new List<DateTime>();

        var workStart = date.ToDateTime(TimeOnly.MinValue).Add(rule.StartTime);
        var workEnd = date.ToDateTime(TimeOnly.MinValue).Add(rule.EndTime);

        var apptsSpec = new BaseSpecifications<Appointment>(a => a.DoctorId == doctorId
                        && a.Status != AppointmentStatus.Cancelled
                        && a.StartDateTime < workEnd
                        && a.EndDateTime > workStart);
        var appts = (await _unitOfWork.Repository<Appointment>()
                .GetAllWithSpecAsync(apptsSpec))
            .Select(a => new { a.StartDateTime, a.EndDateTime })
            .ToList();

        var step = TimeSpan.FromMinutes(visitMinutes);
        var slots = new List<DateTime>();

        for (var t = workStart; t.Add(step) <= workEnd; t = t.Add(step))
        {
            var end = t.Add(step);
            var overlaps = appts.Any(a => t < a.EndDateTime && end > a.StartDateTime);
            if (!overlaps) slots.Add(t);
        }

        return slots;
    }
}
