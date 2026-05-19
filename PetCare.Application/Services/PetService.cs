namespace PetCare.Application.Services;
using PetCare.Domain.Entities;
using PetCare.Application.Interfaces;

public class PetService : IPetService
{
    private readonly IPetRepository _repository;

    public PetService(IPetRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Pet> GetAll()
    {
        return _repository.GetAll();
    }

    public Pet GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Create(Pet pet)
    {
        _repository.Create(pet);
    }
}