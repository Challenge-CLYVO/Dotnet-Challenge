namespace PetCare.Application.DTOs.HistoricoSaude;

public class CreateHistoricoSaudeDto
{
    public string Descricao { get; set; } = string.Empty;

    public DateTime DataRegistro { get; set; }

    public int IdPet { get; set; }
}