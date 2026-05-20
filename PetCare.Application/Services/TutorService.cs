using PetCare.Application.DTOs.Tutor;
using PetCare.Application.Interfaces;
using PetCare.Domain.Entities;

namespace PetCare.Application.Services;

public class TutorService : ITutorService
{
    private readonly ITutorRepository _repository;

    public TutorService(ITutorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReadTutorDto>> GetAllAsync()
    {
        var tutores = await _repository.GetAllAsync();

        return tutores.Select(t => new ReadTutorDto
        {
            IdTutor = t.IdTutor,
            Nome = t.Nome,
            Telefone = t.Telefone,
            Email = t.Email
        });
    }

    public async Task<ReadTutorDto?> GetByIdAsync(int id)
    {
        var tutor = await _repository.GetByIdAsync(id);

        if (tutor == null)
            return null;

        return new ReadTutorDto
        {
            IdTutor = tutor.IdTutor,
            Nome = tutor.Nome,
            Telefone = tutor.Telefone,
            Email = tutor.Email
        };
    }

    public async Task CreateAsync(CreateTutorDto dto)
    {
        var tutor = new Tutor
        {
            Nome = dto.Nome,
            Telefone = dto.Telefone,
            Email = dto.Email
        };

        await _repository.AddAsync(tutor);
    }

    public async Task UpdateAsync(int id, UpdateTutorDto dto)
    {
        var tutor = await _repository.GetByIdAsync(id);

        if (tutor == null)
            return;

        tutor.Nome = dto.Nome;
        tutor.Telefone = dto.Telefone;
        tutor.Email = dto.Email;

        await _repository.UpdateAsync(tutor);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}