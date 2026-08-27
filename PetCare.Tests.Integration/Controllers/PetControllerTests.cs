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
        var id = 21;

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
}