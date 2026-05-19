using Microsoft.AspNetCore.Mvc;
using PetCare.Application.Interfaces;
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
    public IActionResult Create(Pet pet)
    {
        _service.Create(pet);
        return Ok();
    }
}