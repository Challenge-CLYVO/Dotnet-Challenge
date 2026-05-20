using PetCare.Application.DTOs.Clinica;
using PetCare.Application.Interfaces;
using PetCare.Domain.Entities;
using PetCare.Application.Exceptions;

namespace PetCare.Application.Services;

public class ClinicaService : IClinicaService
{
    private readonly IClinicaRepository _repository;

    public ClinicaService(IClinicaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReadClinicaDto>> GetAllAsync()
    {
        var clinicas = await _repository.GetAllAsync();

        return clinicas.Select(c => new ReadClinicaDto
        {
            IdClinica = c.IdClinica,
            Nome = c.Nome,
            Endereco = c.Endereco,
            Telefone = c.Telefone
        });
    }

    public async Task<ReadClinicaDto?> GetByIdAsync(int id)
    {
        var clinica = await _repository.GetByIdAsync(id);

        if (clinica == null)
            throw new NotFoundException("Clínica não encontrada.");

        return new ReadClinicaDto
        {
            IdClinica = clinica.IdClinica,
            Nome = clinica.Nome,
            Endereco = clinica.Endereco,
            Telefone = clinica.Telefone
        };
    }

    public async Task CreateAsync(CreateClinicaDto dto)
    {
        var clinica = new Clinica
        {
            Nome = dto.Nome,
            Endereco = dto.Endereco,
            Telefone = dto.Telefone
        };

        await _repository.AddAsync(clinica);
    }

    public async Task UpdateAsync(int id, UpdateClinicaDto dto)
    {
        var clinica = await _repository.GetByIdAsync(id);

        if (clinica == null)
            throw new NotFoundException("Clínica não encontrada.");

        clinica.Nome = dto.Nome;
        clinica.Endereco = dto.Endereco;
        clinica.Telefone = dto.Telefone;

        await _repository.UpdateAsync(clinica);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}