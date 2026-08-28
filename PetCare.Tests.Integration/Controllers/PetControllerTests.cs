using System.Net;
using System.Net.Http.Json;
using PetCare.Application.DTOs.Pet;
using PetCare.Tests.Integration.Fixtures;

namespace PetCare.Tests.Integration.Controllers;

public class PetControllerTests : IClassFixture<PetCareApiFactory>
{
    private readonly HttpClient _client;

    public PetControllerTests(PetCareApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllAsync_RequisicaoValida_RetornaSucesso()
    {
        // Arrange
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            "/api/pet");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task GetById_PetExistente_RetornaSucesso()
    {
        // Arrange
        var id = 1;

        // Act
        var response = await _client.GetAsync(
            $"/api/Pet/{id}");

        // Assert
        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task GetById_PetNaoExistente_RetornaNotFound()
    {
        // Arrange
        var id = 999999;

        // Act
        var response = await _client.GetAsync(
            $"/api/Pet/{id}");

        // Assert
        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task Create_PetValido_RetornaCreated()
    {
        // Arrange
        var dto = new CreatePetDto
        {
            Nome = "Pet Teste Integracao",
            Idade = 3,
            Especie = "Cachorro",
            Raca = "Labrador",
            IdTutor = 1
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/Pet",
            dto);

        // Assert
        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);
    }

    [Fact]
    public async Task Create_DadosInvalidos_RetornaBadRequest()
    {
        // Arrange
        var dto = new CreatePetDto
        {
            Nome = "",
            Idade = 100,
            Especie = "",
            Raca = "Teste",
            IdTutor = 1
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/Pet",
            dto);

        // Assert
        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }
    
    [Fact]
    public async Task Update_PetExistente_RetornaNoContent()
    {
        // Arrange
        var id = 1;

        var dto = new UpdatePetDto
        {
            Nome = "Pet Atualizado Integracao",
            Idade = 4,
            Especie = "Cachorro",
            Raca = "Labrador",
            IdTutor = 1
        };

        // Act
        var response = await _client.PutAsJsonAsync(
            $"/api/Pet/{id}",
            dto);

        // Assert
        Assert.Equal(
            HttpStatusCode.NoContent,
            response.StatusCode);
    }
    
    [Fact]
    public async Task Update_PetNaoExistente_RetornaNotFound()
    {
        // Arrange
        var id = 999999;
    
        var dto = new UpdatePetDto
        {
            Nome = "Pet Inexistente",
            Idade = 4,
            Especie = "Cachorro",
            Raca = "Labrador",
            IdTutor = 1
        };
    
        // Act
        var response = await _client.PutAsJsonAsync(
            $"/api/Pet/{id}",
            dto);
    
        // Assert
        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_PetExistente_RetornaNoContent()
    {
        // Arrange
        var id = 2;

        // Act
        var response = await _client.DeleteAsync(
            $"/api/Pet/{id}");

        // Assert
        Assert.Equal(
            HttpStatusCode.NoContent,
            response.StatusCode);
    }
}

