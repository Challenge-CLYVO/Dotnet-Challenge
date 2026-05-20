using Microsoft.AspNetCore.Mvc;
using PetCare.Application.DTOs.Vacina;
using PetCare.Application.Interfaces;

namespace PetCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacinaController : ControllerBase
{
    private readonly IVacinaService _service;

    public VacinaController(IVacinaService service)
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
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVacinaDto dto)
    {
        await _service.CreateAsync(dto);

        return Created("", dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateVacinaDto dto)
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