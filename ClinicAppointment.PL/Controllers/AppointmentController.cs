namespace ClinicAppointment.PL.Controllers;
public class AppointmentController : ClinicAppointmentController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISchedulingService _schedulingService;

    public AppointmentController(IUnitOfWork unitOfWork, ISchedulingService schedulingService)
    {
        _unitOfWork = unitOfWork;
        _schedulingService = schedulingService;
    }

    public async Task<IActionResult> Index()
    {
        var spec = new BaseSpecifications<Appointment>();
        spec.Includes.Add(a => a.Doctor);
        spec.Includes.Add(a => a.Patient);
        spec.AddOrderByDesc(a => a.StartDateTime);
        var data = (await _unitOfWork.Repository<Appointment>().GetAllWithSpecAsync(spec))
            .Select(a => new AppointmentRowViewModel
            {
                Id = a.Id,
                PatientName = a.Patient.FullName,
                DoctorName = a.Doctor.FullName,
                Start = a.StartDateTime,
                End = a.EndDateTime,
                Status = a.Status.ToString()
            })
            .ToList();

        return View(data);
    }

    public async Task<IActionResult> Create()
    {
        var doctorSpec = new BaseSpecifications<Doctor>();
        doctorSpec.AddOrderBy(d => d.FullName);
        var patientSpec = new BaseSpecifications<Patient>();
        patientSpec.AddOrderBy(d => d.FullName);
        var vm = new CreateAppointmentViewModel
        {
            AppointmentDate = DateOnly.FromDateTime(DateTime.Today),
            Doctors = (await _unitOfWork.Repository<Doctor>().GetAllWithSpecAsync(doctorSpec)).
                Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.FullName }).ToList(),
            Patients = (await _unitOfWork.Repository<Patient>().GetAllWithSpecAsync(patientSpec)).
                Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.FullName }).ToList(),
        };

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> FreeSlots(int doctorId, DateOnly date, int visitMinutes = 30)
    {
        var slots = await _schedulingService.GetFreeSlotsAsync(doctorId, date, visitMinutes);


        var now = DateTime.Now; 
        if (date == DateOnly.FromDateTime(now))
        {
            var cutoff = now.AddMinutes(1); 
            slots = slots
                .Where(s => s >= cutoff)
                .ToList();
        }

        var result = slots.Select(s => new
        {
            value = s.ToString("O"),       
            text = s.ToString("HH:mm")
        });
        return Json(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAppointmentViewModel vm)
    {
        var spec = new BaseSpecifications<Doctor>();
        spec.AddOrderBy(d => d.FullName);
        vm.Doctors = (await _unitOfWork.Repository<Doctor>().GetAllWithSpecAsync(spec)).
                Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.FullName }).ToList();

        if (!ModelState.IsValid) return View(vm);

        if (vm.SelectedStartDateTime is null)
        {
            ModelState.AddModelError("", "Please select a free slot.");
            return View(vm);
        }

        var start = vm.SelectedStartDateTime.Value;
        var end = start.AddMinutes(vm.VisitMinutes);

        var appointmentSpec = new BaseSpecifications<Appointment>(a =>
            a.DoctorId == vm.DoctorId
            && a.Status != AppointmentStatus.Cancelled
            && start < a.EndDateTime && end > a.StartDateTime);

        var conflict = await _unitOfWork.Repository<Appointment>().AnyWithSpecAsync(appointmentSpec);

        if (conflict)
        {
            ModelState.AddModelError("", "This slot is no longer available. Please refresh slots.");
            return View(vm);
        }

        var appt = new Appointment
        {
            DoctorId = vm.DoctorId,
            PatientId = vm.PatientId,
            StartDateTime = start,
            EndDateTime = end,
            VisitMinutes = vm.VisitMinutes,
            Status = AppointmentStatus.Booked,
            CreationDate = DateTime.Now
        };

        _unitOfWork.Repository<Appointment>().Add(appt);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Appointment Created Successfully";
        return RedirectToAction(nameof(Index));
    }
}
