namespace PetCare.Application.Interfaces;

using PetCare.Domain.Entities;

public interface IPetRepository
{
    IEnumerable<Pet> GetAll();
    Pet GetById(int id);
    void Create(Pet pet);
}