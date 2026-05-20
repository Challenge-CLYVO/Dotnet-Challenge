namespace PetCare.Application.DTOs.AplicacaoVacina;

public class UpdateAplicacaoVacinaDto
{
    public DateTime DataAplicacao { get; set; }

    public int IdVacina { get; set; }

    public int IdPet { get; set; }
}