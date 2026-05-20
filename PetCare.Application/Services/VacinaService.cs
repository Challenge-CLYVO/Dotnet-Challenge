using PetCare.Application.DTOs.Vacina;
using PetCare.Application.Interfaces;
using PetCare.Domain.Entities;
using PetCare.Application.Exceptions;

namespace PetCare.Application.Services;

public class VacinaService : IVacinaService
{
    private readonly IVacinaRepository _repository;

    public VacinaService(IVacinaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReadVacinaDto>> GetAllAsync()
    {
        var vacinas = await _repository.GetAllAsync();

        return vacinas.Select(v => new ReadVacinaDto
        {
            IdVacina = v.IdVacina,
            Nome = v.Nome,
            Descricao = v.Descricao
        });
    }

    public async Task<ReadVacinaDto?> GetByIdAsync(int id)
    {
        var vacina = await _repository.GetByIdAsync(id);

        if (vacina == null)
            throw new NotFoundException("Vacina não encontrada.");

        return new ReadVacinaDto
        {
            IdVacina = vacina.IdVacina,
            Nome = vacina.Nome,
            Descricao = vacina.Descricao
        };
    }

    public async Task CreateAsync(CreateVacinaDto dto)
    {
        var vacina = new Vacina
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };

        await _repository.AddAsync(vacina);
    }

    public async Task UpdateAsync(int id, UpdateVacinaDto dto)
    {
        var vacina = await _repository.GetByIdAsync(id);

        if (vacina == null)
            throw new NotFoundException("Vacina não encontrada.");

        vacina.Nome = dto.Nome;
        vacina.Descricao = dto.Descricao;

        await _repository.UpdateAsync(vacina);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}