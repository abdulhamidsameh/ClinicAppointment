namespace ClinicAppointment.PL.Controllers;
public class DoctorScheduleRuleController : ClinicAppointmentController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DoctorScheduleRuleController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IActionResult> Index()
    {
        var spec = new BaseSpecifications<DoctorScheduleRule>(d => !d.IsDeleted);
        spec.Includes.Add(d => d.Doctor);
        var DoctorScheduleRules = await _unitOfWork.Repository<DoctorScheduleRule>().GetAllWithSpecAsync(spec);
        var mappedDoctorScheduleRules = _mapper.Map<IEnumerable<DoctorScheduleRule>, IEnumerable<DoctorScheduleRuleViewModel>>(DoctorScheduleRules);
        return View(mappedDoctorScheduleRules);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(DoctorScheduleRuleViewModel doctorScheduleRuleViewModel)
    {
        if (!ModelState.IsValid)
            return View(doctorScheduleRuleViewModel);
        var mappedDoctorScheduleRuleViewModel = _mapper.Map<DoctorScheduleRuleViewModel, DoctorScheduleRule>(doctorScheduleRuleViewModel);
        _unitOfWork.Repository<DoctorScheduleRule>().Add(mappedDoctorScheduleRuleViewModel);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Doctor Schedule Rule Created Successfully";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Details(int? id, string viewName = "Details")
    {
        if (!id.HasValue)
            return BadRequest(); // 400
        var spec = new BaseSpecifications<DoctorScheduleRule>(d => !d.IsDeleted);
        spec.Includes.Add(d => d.Doctor);
        var doctorScheduleRuleViewModel = await _unitOfWork.Repository<DoctorScheduleRule>().GetWithSpecAsync(spec);
        if (doctorScheduleRuleViewModel is null)
            return NotFound(); // 404
        var mappedDoctorScheduleRuleViewModel = _mapper.Map<DoctorScheduleRule, DoctorScheduleRuleViewModel>(doctorScheduleRuleViewModel);
        return View(viewName, mappedDoctorScheduleRuleViewModel);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        return await Details(id, "Edit");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, DoctorScheduleRuleViewModel doctorScheduleRuleViewModel)
    {
        if (id != doctorScheduleRuleViewModel.Id)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(doctorScheduleRuleViewModel);
        var mappedDoctorScheduleRuleViewModel = _mapper.Map<DoctorScheduleRuleViewModel, DoctorScheduleRule>(doctorScheduleRuleViewModel);
        _unitOfWork.Repository<DoctorScheduleRule>().Update(mappedDoctorScheduleRuleViewModel);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Doctor Schedule Rule Updated Successfully";
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id)
    {
        return await Details(id, "Delete");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromRoute] int id, DoctorScheduleRuleViewModel doctorScheduleRuleViewModel)
    {
        if (id != doctorScheduleRuleViewModel.Id)
            return BadRequest();
        var doctorScheduleRule = await _unitOfWork.Repository<DoctorScheduleRule>().GetByIdAsync(id);
        if (doctorScheduleRule is null)
            return NotFound();
        doctorScheduleRule.IsDeleted = true;
        _unitOfWork.Repository<DoctorScheduleRule>().Update(doctorScheduleRule);
        await _unitOfWork.CompleteAsync();
        TempData["Message"] = "Doctor Schedule Rule Deleted Successfully";
        return RedirectToAction(nameof(Index));
    }
}
