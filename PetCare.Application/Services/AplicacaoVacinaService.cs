using PetCare.Application.DTOs.AplicacaoVacina;
using PetCare.Domain.Entities;
using PetCare.Application.Interfaces;
using PetCare.Application.Exceptions;

namespace PetCare.Application.Services;

public class AplicacaoVacinaService
{
    private readonly IAplicacaoVacinaRepository _repository;

    public AplicacaoVacinaService(IAplicacaoVacinaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReadAplicacaoVacinaDto>> GetAllAsync()
    {
        var aplicacoes = await _repository.GetAllAsync();

        return aplicacoes.Select(a => new ReadAplicacaoVacinaDto
        {
            IdAplicacao = a.IdAplicacao,
            DataAplicacao = a.DataAplicacao,
            IdVacina = a.IdVacina,
            IdPet = a.IdPet
        });
    }

    public async Task<ReadAplicacaoVacinaDto?> GetByIdAsync(int id)
    {
        var a = await _repository.GetByIdAsync(id);

        if (a == null)
            throw new NotFoundException("Aplicação de vacina não encontrada.");

        return new ReadAplicacaoVacinaDto
        {
            IdAplicacao = a.IdAplicacao,
            DataAplicacao = a.DataAplicacao,
            IdVacina = a.IdVacina,
            IdPet = a.IdPet
        };
    }

    public async Task CreateAsync(CreateAplicacaoVacinaDto dto)
    {
        var aplicacao = new AplicacaoVacina
        {
            DataAplicacao = dto.DataAplicacao,
            IdVacina = dto.IdVacina,
            IdPet = dto.IdPet
        };

        await _repository.AddAsync(aplicacao);
    }

    public async Task UpdateAsync(int id, UpdateAplicacaoVacinaDto dto)
    {
        var aplicacao = await _repository.GetByIdAsync(id);

        if (aplicacao == null)
            throw new NotFoundException("Aplicação de vacina não encontrada.");

        aplicacao.DataAplicacao = dto.DataAplicacao;
        aplicacao.IdVacina = dto.IdVacina;
        aplicacao.IdPet = dto.IdPet;

        await _repository.UpdateAsync(aplicacao);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}