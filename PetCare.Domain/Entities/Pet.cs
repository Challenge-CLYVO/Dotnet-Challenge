namespace PetCare.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class Pet
{
    [Key]
    public int IdPet { get; set; }

    public string Nome { get; set; }
    public int IdTutor { get; set; }

    public Tutor? Tutor { get; set; }
}