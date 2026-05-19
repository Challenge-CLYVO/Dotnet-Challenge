using Microsoft.AspNetCore.Mvc;
using PetCare.Application.Interfaces;
using PetCare.Application.DTOs;
using PetCare.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class PetController : ControllerBase
{
    private readonly IPetService _service;

    public PetController(IPetService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_service.GetAll());
    }

    [HttpPost]
    public IActionResult Create(CreatePetDto dto)
    {
        var pet = new Pet
        {
            Nome = dto.Nome,
            IdTutor = dto.IdTutor
        };

        _service.Create(pet);
        return Ok();
    }
}