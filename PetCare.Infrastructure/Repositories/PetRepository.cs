namespace PetCare.Infrastructure.Repositories;

using PetCare.Domain.Entities;
using PetCare.Application.Interfaces;
using PetCare.Infrastructure.Data;

public class PetRepository : IPetRepository
{
    private readonly AppDbContext _context;

    public PetRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Pet> GetAll()
    {
        return _context.Pets.ToList();
    }

    public Pet GetById(int id)
    {
        return _context.Pets.Find(id);
    }

    public void Create(Pet pet)
    {
        _context.Pets.Add(pet);
        _context.SaveChanges();
    }
}