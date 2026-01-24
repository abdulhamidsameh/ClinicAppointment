namespace ClinicAppointment.PL.Controllers;
public class ClinicController : ClinicAppointmentController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClinicController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        var spec = new BaseSpecifications<Clinic>(c => !c.IsDeleted);
        var clinics = await _unitOfWork.Repository<Clinic>().GetAllWithSpecAsync(spec);
        var mappedClinics = _mapper.Map<IEnumerable<Clinic>, IEnumerable<ClinicViewModel>>(clinics);
        return View(mappedClinics);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ClinicViewModel clinicViewModel)
    {
        if (!ModelState.IsValid)
            return View(clinicViewModel);
        var mappedClinic = _mapper.Map<ClinicViewModel,Clinic>(clinicViewModel);
        _unitOfWork.Repository<Clinic>().Add(mappedClinic);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Clinic Created Successfully";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Details(int? id, string viewName = "Details")
    {
        if (!id.HasValue)
            return BadRequest(); // 400
        var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id.Value);
        if (clinic is null)
            return NotFound(); // 404
        var mappedClinic = _mapper.Map<Clinic,ClinicViewModel>(clinic);
        return View(viewName, mappedClinic);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        return await Details(id, "Edit");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, ClinicViewModel clinicViewModel)
    {
        if (id != clinicViewModel.Id)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(clinicViewModel);
        var mappedClinic = _mapper.Map<ClinicViewModel,Clinic>(clinicViewModel);
        _unitOfWork.Repository<Clinic>().Update(mappedClinic);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Clinic Updated Successfully";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id)
    {
        return await Details(id, "Delete");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromRoute] int id, ClinicViewModel clinicViewModel)
    {
        if (id != clinicViewModel.Id)
            return BadRequest();
        var spec = new BaseSpecifications<Doctor>(d => d.ClinicId == id && !d.IsDeleted);
        var hasDoctors = await _unitOfWork.Repository<Doctor>().AnyWithSpecAsync(spec);
        if (hasDoctors)
        {
            TempData["Message"] = "Cannot delete clinic. It has active doctors.";
            return RedirectToAction(nameof(Index));
        }
        var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id);
        if (clinic is null)
            return NotFound();
        clinic.IsDeleted = true;
        _unitOfWork.Repository<Clinic>().Update(clinic);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Clinic Deleted Successfully";
        return RedirectToAction(nameof(Index));
    }
}