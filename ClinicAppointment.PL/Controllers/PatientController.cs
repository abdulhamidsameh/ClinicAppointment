namespace ClinicAppointment.PL.Controllers;
public class PatientController : ClinicAppointmentController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatientController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        var spec = new BaseSpecifications<Patient>(d => !d.IsDeleted);
        var Patients = await _unitOfWork.Repository<Patient>().GetAllWithSpecAsync(spec);
        var mappedPatients = _mapper.Map<IEnumerable<Patient>, IEnumerable<PatientViewModel>>(Patients);
        return View(mappedPatients);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(PatientViewModel PatientViewModel)
    {
        if (!ModelState.IsValid)
            return View(PatientViewModel);
        var mappedPatient = _mapper.Map<PatientViewModel, Patient>(PatientViewModel);
        _unitOfWork.Repository<Patient>().Add(mappedPatient);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Patient Created Successfully";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Details(int? id, string viewName = "Details")
    {
        if (!id.HasValue)
            return BadRequest(); // 400
        var spec = new BaseSpecifications<Patient>(d => !d.IsDeleted);
        var Patient = await _unitOfWork.Repository<Patient>().GetWithSpecAsync(spec);
        if (Patient is null)
            return NotFound(); // 404
        var mappedPatient = _mapper.Map<Patient, PatientViewModel>(Patient);
        return View(viewName, mappedPatient);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        return await Details(id, "Edit");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, PatientViewModel PatientViewModel)
    {
        if (id != PatientViewModel.Id)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(PatientViewModel);
        var mappedPatient = _mapper.Map<PatientViewModel, Patient>(PatientViewModel);
        _unitOfWork.Repository<Patient>().Update(mappedPatient);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Patient Updated Successfully";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id)
    {
        return await Details(id, "Delete");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromRoute] int id, PatientViewModel PatientViewModel)
    {
        if (id != PatientViewModel.Id)
            return BadRequest();
        var Patient = await _unitOfWork.Repository<Patient>().GetByIdAsync(id);
        if (Patient is null)
            return NotFound();
        Patient.IsDeleted = true;
        _unitOfWork.Repository<Patient>().Update(Patient);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Patient Deleted Successfully";
        return RedirectToAction(nameof(Index));
    }
}
