
using DemoLiteDB.Models;
using DemoLiteDB.PatientRegister;
using Microsoft.AspNetCore.Mvc;

namespace DemoLiteDB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly PatientRegister _patientService;

    public PatientController(PatientRegister patientService) =>
        _patientService = patientService;

    [HttpGet]
    public async Task<List<Patient>> Get() =>
        await _patientService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Patient>> GetAsync(int id)
    {
        var book = await _patientService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Patient newBook)
    {
        await _patientService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }




}