using Microsoft.EntityFrameworkCore;
using PetCare.Domain.Entities;

namespace PetCare.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Tutor> Tutores { get; set; }
    public DbSet<Pet> Pets { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}