using Microsoft.AspNetCore.Mvc;
using PetCare.Application.DTOs.Consulta;
using PetCare.Application.Interfaces;

namespace PetCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultaController : ControllerBase
{
    private readonly IConsultaService _service;

    public ConsultaController(IConsultaService service)
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
        var consulta = await _service.GetByIdAsync(id);

        if (consulta == null)
            return NotFound();

        return Ok(consulta);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateConsultaDto dto)
    {
        await _service.CreateAsync(dto);

        return Created("", dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateConsultaDto dto)
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