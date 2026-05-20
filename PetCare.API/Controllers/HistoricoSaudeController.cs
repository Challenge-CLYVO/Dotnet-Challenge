using Microsoft.AspNetCore.Mvc;
using PetCare.Application.DTOs.HistoricoSaude;
using PetCare.Application.Interfaces;

namespace PetCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoricoSaudeController : ControllerBase
{
    private readonly IHistoricoSaudeService _service;

    public HistoricoSaudeController(IHistoricoSaudeService service)
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
        var historico = await _service.GetByIdAsync(id);

        if (historico == null)
            return NotFound();

        return Ok(historico);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateHistoricoSaudeDto dto)
    {
        await _service.CreateAsync(dto);

        return Created("", dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateHistoricoSaudeDto dto)
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