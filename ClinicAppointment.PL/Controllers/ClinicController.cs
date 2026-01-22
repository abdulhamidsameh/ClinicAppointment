using System.Threading.Tasks;

namespace ClinicAppointment.PL.Controllers;
public class ClinicController : ClinicAppointmentController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _env;

    public ClinicController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _env = env;
    }
    public async Task<IActionResult> Index()
    {
        var spec = new BaseSpecifications<Clinic>(c => !c.IsDeleted);
        var clinics = await _unitOfWork.Repository<Clinic>().GetAllWithSpecAsync(spec);
        return View(clinics);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Clinic clinic)
    {
        if (!ModelState.IsValid)
            return View(clinic);
        _unitOfWork.Repository<Clinic>().Add(clinic);
        await _unitOfWork.CompleteAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Details(int? id, string viewName = "Details")
    {
        if (!id.HasValue)
            return BadRequest(); // 400
        var clinic = await _unitOfWork.Repository<Clinic>().GetByIdAsync(id.Value);
        if (clinic is null)
            return NotFound(); // 404
        return View(viewName, clinic);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        return await Details(id, "Edit");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, Clinic clinic)
    {
        if (id != clinic.Id)
            return BadRequest();
        if (!ModelState.IsValid)
            return View(clinic);
        _unitOfWork.Repository<Clinic>().Update(clinic);
        await _unitOfWork.CompleteAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id)
    {
        return await Details(id, "Delete");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete([FromRoute] int id, Clinic clinic)
    {
        if (id != clinic.Id)
            return BadRequest();
        clinic.IsDeleted = true;
        _unitOfWork.Repository<Clinic>().Update(clinic);
        await _unitOfWork.CompleteAsync();
        return RedirectToAction(nameof(Index));
    }
}