namespace PetCare.Application.DTOs.Consulta;

public class CreateConsultaDto
{
    public DateTime DataConsulta { get; set; }

    public string? Descricao { get; set; }

    public int IdPet { get; set; }

    public int IdClinica { get; set; }
}