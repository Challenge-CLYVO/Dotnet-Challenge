using Microsoft.AspNetCore.Mvc;
using PetCare.Application.DTOs.Clinica;
using PetCare.Application.Interfaces;

namespace PetCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicaController : ControllerBase
{
    private readonly IClinicaService _service;

    public ClinicaController(IClinicaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var clinica = await _service.GetByIdAsync(id);

        if (clinica == null)
            return NotFound();

        return Ok(clinica);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClinicaDto dto)
    {
        await _service.CreateAsync(dto);

        return Created("", dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateClinicaDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}