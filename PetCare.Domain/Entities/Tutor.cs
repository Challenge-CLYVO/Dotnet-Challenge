namespace PetCare.Domain.Entities;

using System.ComponentModel.DataAnnotations;

public class Tutor
{
    [Key]
    public int IdTutor { get; set; }

    public string Nome { get; set; }
    public ICollection<Pet>? Pets { get; set; }
}