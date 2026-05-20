using PetCare.Application.DTOs.Pet;
using PetCare.Application.Interfaces;
using PetCare.Domain.Entities;

namespace PetCare.Application.Services;

public class PetService : IPetService
{
    private readonly IPetRepository _repository;

    public PetService(IPetRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReadPetDto>> GetAllAsync()
    {
        var pets = await _repository.GetAllAsync();

        return pets.Select(p => new ReadPetDto
        {
            IdPet = p.IdPet,
            Nome = p.Nome,
            Idade = p.Idade,
            Especie = p.Especie,
            Raca = p.Raca,
            IdTutor = p.IdTutor,
            NomeTutor = p.Tutor.Nome
        });
    }

    public async Task<ReadPetDto?> GetByIdAsync(int id)
    {
        var pet = await _repository.GetByIdAsync(id);

        if (pet == null)
            return null;

        return new ReadPetDto
        {
            IdPet = pet.IdPet,
            Nome = pet.Nome,
            Idade = pet.Idade,
            Especie = pet.Especie,
            Raca = pet.Raca,
            IdTutor = pet.IdTutor,
            NomeTutor = pet.Tutor.Nome
        };
    }

    public async Task CreateAsync(CreatePetDto dto)
    {
        var pet = new Pet
        {
            Nome = dto.Nome,
            Idade = dto.Idade,
            Especie = dto.Especie,
            Raca = dto.Raca,
            IdTutor = dto.IdTutor
        };

        await _repository.AddAsync(pet);
    }

    public async Task UpdateAsync(int id, UpdatePetDto dto)
    {
        var pet = await _repository.GetByIdAsync(id);

        if (pet == null)
            return;

        pet.Nome = dto.Nome;
        pet.Idade = dto.Idade;
        pet.Especie = dto.Especie;
        pet.Raca = dto.Raca;
        pet.IdTutor = dto.IdTutor;

        await _repository.UpdateAsync(pet);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}