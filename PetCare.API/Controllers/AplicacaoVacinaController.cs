using Microsoft.AspNetCore.Mvc;
using PetCare.Application.DTOs.AplicacaoVacina;
using PetCare.Application.Services;

namespace PetCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AplicacaoVacinaController : ControllerBase
{
    private readonly AplicacaoVacinaService _service;

    public AplicacaoVacinaController(AplicacaoVacinaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var aplicacoes = await _service.GetAllAsync();
        return Ok(aplicacoes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var aplicacao = await _service.GetByIdAsync(id);

        if (aplicacao == null)
            return NotFound();

        return Ok(aplicacao);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAplicacaoVacinaDto dto)
    {
        await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = 0 }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateAplicacaoVacinaDto dto)
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