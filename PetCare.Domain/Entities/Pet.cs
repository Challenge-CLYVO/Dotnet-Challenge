namespace PetCare.Domain.Entities;

public class Pet
{
    public int IdPet { get; set; }
    public string Nome { get; set; }
    public int? Idade { get; set; }
    public string Especie { get; set; }
    public string Raca { get; set; }

    public int IdTutor { get; set; }
    public Tutor Tutor { get; set; }
}