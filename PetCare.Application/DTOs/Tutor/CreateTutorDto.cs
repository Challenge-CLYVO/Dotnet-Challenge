namespace PetCare.Application.DTOs.Tutor;

public class CreateTutorDto
{
    public string Nome { get; set; } = string.Empty;

    public string? Telefone { get; set; }

    public string? Email { get; set; }
}