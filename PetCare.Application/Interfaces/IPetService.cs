using PetCare.Domain.Entities;

public interface IPetService
{
    IEnumerable<Pet> GetAll();
    Pet GetById(int id);
    void Create(Pet pet);
}