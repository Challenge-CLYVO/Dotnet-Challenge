using PetCare.Application.DTOs.Consulta;
using PetCare.Application.Interfaces;
using PetCare.Domain.Entities;
using PetCare.Application.Interfaces;

namespace PetCare.Application.Services;

public class ConsultaService : IConsultaService
{
    private readonly IConsultaRepository _repository;

    public ConsultaService(IConsultaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReadConsultaDto>> GetAllAsync()
    {
        var consultas = await _repository.GetAllAsync();

        return consultas.Select(c => new ReadConsultaDto
        {
            IdConsulta = c.IdConsulta,
            DataConsulta = c.DataConsulta,
            Descricao = c.Descricao,
            IdPet = c.IdPet,
            IdClinica = c.IdClinica
        });
    }

    public async Task<ReadConsultaDto?> GetByIdAsync(int id)
    {
        var consulta = await _repository.GetByIdAsync(id);

        if (consulta == null)
            return null;

        return new ReadConsultaDto
        {
            IdConsulta = consulta.IdConsulta,
            DataConsulta = consulta.DataConsulta,
            Descricao = consulta.Descricao,
            IdPet = consulta.IdPet,
            IdClinica = consulta.IdClinica
        };
    }

    public async Task CreateAsync(CreateConsultaDto dto)
    {
        var consulta = new Consulta
        {
            DataConsulta = dto.DataConsulta,
            Descricao = dto.Descricao,
            IdPet = dto.IdPet,
            IdClinica = dto.IdClinica
        };

        await _repository.AddAsync(consulta);
    }

    public async Task UpdateAsync(int id, UpdateConsultaDto dto)
    {
        var consulta = await _repository.GetByIdAsync(id);

        if (consulta == null)
            return;

        consulta.DataConsulta = dto.DataConsulta;
        consulta.Descricao = dto.Descricao;
        consulta.IdPet = dto.IdPet;
        consulta.IdClinica = dto.IdClinica;

        await _repository.UpdateAsync(consulta);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}