namespace PetCare.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using PetCare.Domain.Entities;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pet> Pets { get; set; }
    public DbSet<Tutor> Tutors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TABELA PET
        modelBuilder.Entity<Pet>(entity =>
        {
            entity.ToTable("Pet");

            entity.HasKey(e => e.IdPet);

            entity.Property(e => e.IdPet).HasColumnName("id_pet");
            entity.Property(e => e.Nome).HasColumnName("nome").IsRequired();
            entity.Property(e => e.Idade).HasColumnName("idade");
            entity.Property(e => e.Especie).HasColumnName("especie").IsRequired();
            entity.Property(e => e.Raca).HasColumnName("raca");
            entity.Property(e => e.IdTutor).HasColumnName("id_tutor");

            entity.HasOne(e => e.Tutor)
                .WithMany(t => t.Pets)
                .HasForeignKey(e => e.IdTutor);
        });

        // TABELA TUTOR
        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.ToTable("Tutor");

            entity.HasKey(e => e.IdTutor);

            entity.Property(e => e.IdTutor).HasColumnName("id_tutor");
            entity.Property(e => e.Nome).HasColumnName("nome").IsRequired();
            entity.Property(e => e.Telefone).HasColumnName("telefone");
            entity.Property(e => e.Email).HasColumnName("email");
        });
    }
}