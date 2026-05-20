namespace PetCare.Application.DTOs.HistoricoSaude;

public class UpdateHistoricoSaudeDto
{
    public string Descricao { get; set; } = string.Empty;

    public DateTime DataRegistro { get; set; }

    public int IdPet { get; set; }
}