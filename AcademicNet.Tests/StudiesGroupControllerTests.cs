using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

public class StudiesGroupControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StudiesGroupControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // teste de contrato
    [Fact]
    public async Task Contract_LoadStudiesGroup_Deve_Retornar_Estrutura_Valida()
    {
        // Arrange
        int studentId = 1; 
        var response = await _client.GetAsync($"/api/studiesgroup/loadStudiesGroup?studentId={studentId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var lista = JsonDocument.Parse(content).RootElement;

        Assert.True(lista.ValueKind == JsonValueKind.Array);
        foreach (var item in lista.EnumerateArray())
        {
            Assert.True(item.TryGetProperty("id", out _));
            Assert.True(item.TryGetProperty("name", out _)); // ajuste conforme modelo
        }
    }

    [Fact]
    public async Task Contract_LoadPostages_Deve_Retornar_Postagens_Validas()
    {
        // Arrange
        int studentId = 1;
        int groupId = 1;
        int page = 1;

        var response = await _client.GetAsync($"/api/studiesgroup/loadPostages?studentId={studentId}&studiesGroupId={groupId}&page={page}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var lista = JsonDocument.Parse(content).RootElement;

        Assert.True(lista.ValueKind == JsonValueKind.Array);
        foreach (var item in lista.EnumerateArray())
        {
            Assert.True(item.TryGetProperty("id", out _));
            Assert.True(item.TryGetProperty("textBody", out _));
            Assert.True(item.TryGetProperty("date", out _));
        }
    }

    // PERFORMANCE - loadStudiesGroup
    [Fact]
    public async Task Performance_LoadStudiesGroup_100_Requisicoes()
    {
        // Arrange
        int studentId = 1;
        int totalRequests = 100;
        var tasks = new Task<HttpResponseMessage>[totalRequests];

        // Act
        var inicio = DateTime.Now;
        for (int i = 0; i < totalRequests; i++)
        {
            tasks[i] = _client.GetAsync($"/api/studiesgroup/loadStudiesGroup?studentId={studentId}");
        }

        await Task.WhenAll(tasks);
        var duracao = DateTime.Now - inicio;

        // Assert
        foreach (var t in tasks)
        {
            Assert.Equal(HttpStatusCode.OK, t.Result.StatusCode);
        }

        Assert.True(duracao.TotalSeconds < 10, $"Execução levou {duracao.TotalSeconds} segundos.");
    }

    // ==============================
    // PERFORMANCE - loadPostages
    // ==============================
    [Fact]
    public async Task Performance_LoadPostages_50_Paginas()
    {
        // Arrange
        int studentId = 1;
        int groupId = 1;
        int totalPages = 50;

        // Act
        var inicio = DateTime.Now;
        for (int page = 1; page <= totalPages; page++)
        {
            var response = await _client.GetAsync($"/api/studiesgroup/loadPostages?studentId={studentId}&studiesGroupId={groupId}&page={page}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        var duracao = DateTime.Now - inicio;

        // Assert
        Assert.True(duracao.TotalSeconds < 15, $"Leitura de {totalPages} páginas levou {duracao.TotalSeconds} segundos.");
    }
}