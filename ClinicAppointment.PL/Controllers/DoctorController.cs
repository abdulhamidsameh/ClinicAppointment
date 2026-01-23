using ClinicAppointment.PL.ViewModels;

namespace ClinicAppointment.PL.Controllers;
public class DoctorController : ClinicAppointmentController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        var spec = new BaseSpecifications<Doctor>(d => !d.IsDeleted);
        spec.Includes.Add(d => d.Clinic);
        var doctors = await _unitOfWork.Repository<Doctor>().GetAllWithSpecAsync(spec);
        var mappedDoctors = _mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorViewModel>>(doctors);
        return View(mappedDoctors);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(DoctorViewModel doctorViewModel)
    {
        if (!ModelState.IsValid)
            return View(doctorViewModel);
        var mappedDoctor = _mapper.Map<DoctorViewModel, Doctor>(doctorViewModel);
        _unitOfWork.Repository<Doctor>().Add(mappedDoctor);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Doctor Created Successfully";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Details(int? id, string viewName = "Details")
    {
        if (!id.HasValue)
            return BadRequest(); // 400
        var spec = new BaseSpecifications<Doctor>(d => !d.IsDeleted);
        spec.Includes.Add(d => d.Clinic);
        var doctor = await _unitOfWork.Repository<Doctor>().GetWithSpecAsync(spec);
        if (doctor is null)
            return NotFound(); // 404
        var mappedDoctor = _mapper.Map<Doctor, DoctorViewModel>(doctor);
        return View(viewName, mappedDoctor);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        return await Details(id, "Edit");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, DoctorViewModel doctorViewModel)
    {
        if (id != doctorViewModel.Id)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(doctorViewModel);
        var mappedDoctor = _mapper.Map<DoctorViewModel, Doctor>(doctorViewModel);
        _unitOfWork.Repository<Doctor>().Update(mappedDoctor);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Doctor Updated Successfully";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id)
    {
        return await Details(id, "Delete");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromRoute] int id, DoctorViewModel doctorViewModel)
    {
        if (id != doctorViewModel.Id)
            return BadRequest();
        var spec = new BaseSpecifications<DoctorScheduleRule>(d => d.DoctorId == id && !d.IsDeleted);
        var hasDoctorScheduleRule = await _unitOfWork.Repository<DoctorScheduleRule>().AnyAsync(spec);
        if (hasDoctorScheduleRule)
        {
            TempData["Message"] = "Cannot delete doctor. It has active Doctor Schedule Rule.";
            return RedirectToAction(nameof(Index));
        }
        var doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(id);
        if (doctor is null) 
            return NotFound();
        doctor.IsDeleted = true;
        _unitOfWork.Repository<Doctor>().Update(doctor);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Doctor Deleted Successfully";
        return RedirectToAction(nameof(Index));
    }
}
