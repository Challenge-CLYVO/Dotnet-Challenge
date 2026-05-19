namespace PetCare.Domain.Entities;

public class Tutor
{
    public int IdTutor { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }

    public List<Pet> Pets { get; set; }
}