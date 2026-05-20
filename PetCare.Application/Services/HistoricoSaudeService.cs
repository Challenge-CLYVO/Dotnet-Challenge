using PetCare.Application.DTOs.HistoricoSaude;
using PetCare.Application.Interfaces;
using PetCare.Domain.Entities;

namespace PetCare.Application.Services;

public class HistoricoSaudeService : IHistoricoSaudeService
{
    private readonly IHistoricoSaudeRepository _repository;

    public HistoricoSaudeService(IHistoricoSaudeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReadHistoricoSaudeDto>> GetAllAsync()
    {
        var historicos = await _repository.GetAllAsync();

        return historicos.Select(h => new ReadHistoricoSaudeDto
        {
            IdHistorico = h.IdHistorico,
            Descricao = h.Descricao,
            DataRegistro = h.DataRegistro,
            IdPet = h.IdPet
        });
    }

    public async Task<ReadHistoricoSaudeDto?> GetByIdAsync(int id)
    {
        var historico = await _repository.GetByIdAsync(id);

        if (historico == null)
            return null;

        return new ReadHistoricoSaudeDto
        {
            IdHistorico = historico.IdHistorico,
            Descricao = historico.Descricao,
            DataRegistro = historico.DataRegistro,
            IdPet = historico.IdPet
        };
    }

    public async Task CreateAsync(CreateHistoricoSaudeDto dto)
    {
        var historico = new HistoricoSaude
        {
            Descricao = dto.Descricao,
            DataRegistro = dto.DataRegistro,
            IdPet = dto.IdPet
        };

        await _repository.AddAsync(historico);
    }

    public async Task UpdateAsync(int id, UpdateHistoricoSaudeDto dto)
    {
        var historico = await _repository.GetByIdAsync(id);

        if (historico == null)
            return;

        historico.Descricao = dto.Descricao;
        historico.DataRegistro = dto.DataRegistro;
        historico.IdPet = dto.IdPet;

        await _repository.UpdateAsync(historico);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}