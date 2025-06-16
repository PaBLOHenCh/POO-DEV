using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Controllers;
using System.Text.Json;
using System.Text;
using System.Net;
using AcademicNet.Models;

public class StudentControllerTests
{
    private readonly HttpClient _client;

    public StudentControllerTests(CustomWebApplicationFactory factory)
    {
        //Cria o cliente de um banco de dados em memória
        _client = factory.CreateClient();
    }

    private StudentCreateDTO FictionalStudent()
    {
        var student = new StudentCreateDTO
        {
            Name = "Aluno 1",
            Password = "senhaSegura123",
            Phone = "1234567890",
            CPF = "123.456.789-00",
            Email = "b3hZ9@example.com",
            Address = new AddressDTO
            {
                Logradouro = "Rua A",
                Numero = "123",
                Cidade = "Cidade A",
                Bairro = "Bairro A",
                CEP = "12345-678",
                Complemento = "Complemento A",
                Estado = "SP",
                Referencia = "Referência A",
            }
        };

        return student;
    }
    [Fact]
    public async Task Deve_Cadastrar_Aluno_Com_Dados_Validos()
    {
        // Arrange
        // Simula StudentCreateDTO com dados válidos
        // Teste para inserção de endereço correto
        
        var studentCreateDto = FictionalStudent();

        var json = JsonSerializer.Serialize(studentCreateDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act: Enviar POST para /student
        var response = await _client.PostAsync("/api/student/create", content);

        // Assert: Verifica se retorna 201 Created
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Nao_Deve_Cadastrar_Aluno_Com_CPF_Duplicado()
    {
        // Arrange
        var studentCreateDto1 = FictionalStudent();
        var studentCreateDto2 = FictionalStudent();

        var json1 = JsonSerializer.Serialize(studentCreateDto1);
        var json2 = JsonSerializer.Serialize(studentCreateDto2);
        var content1 = new StringContent(json1, Encoding.UTF8, "application/json");
        var content2 = new StringContent(json2, Encoding.UTF8, "application/json");

        // Act
        var response1 = await _client.PostAsync("/api/student/create", content1); // primeiro envio
        var response2 = await _client.PostAsync("/api/student/create", content2); // tentativa duplicada

        // Assert
        Assert.Equal(HttpStatusCode.OK, response1.StatusCode); // primeiro deve funcionar
        Assert.Equal(HttpStatusCode.BadRequest, response2.StatusCode); // segundo deve falhar
    }


    [Fact]
    public async Task Deve_Retornar_Aluno_Por_Id()
    {
        // Arrange: criar aluno e desserializar a resposta para obter o id
        StudentCreateDTO studentCreateDto = FictionalStudent();
        var content = new StringContent(JsonSerializer.Serialize(studentCreateDto), Encoding.UTF8, "application/json");
        var post_response = await _client.PostAsync("/api/student/create", content);

        post_response.EnsureSuccessStatusCode();
        var post_body = await post_response.Content.ReadAsStringAsync();

        var alunoCriado = JsonSerializer.Deserialize<StudentModel>(post_body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Act
        var get_response = await _client.GetAsync($"api/student/{alunoCriado.Id}");

        // Assert: Verifica se o corpo da requisição get retorna um aluno não nulo
        get_response.EnsureSuccessStatusCode();
        var get_body = await get_response.Content.ReadAsStringAsync();
        Assert.NotEmpty(get_body);
    }

    [Fact]
    public async Task Deve_Retornar_404_Para_Aluno_Inexistente()
    {
        // Arrange
        // Act: GET /student/9999

        var get_response = await _client.GetAsync($"api/student/9999");

        // Assert: Verifica 404 NotFound
        Assert.Equal(HttpStatusCode.NotFound, get_response.StatusCode);
    }
}


public class SubjectControllerTests
{
    [Fact]
    public async Task Deve_Criar_Disciplina_E_Associar_Turma()
    {
        // Arrange
        // Act: POST /subject
        // Assert: Verifica associação
    }

    [Fact]
    public async Task Nao_Deve_Associar_Disciplina_Duplicada()
    {
        // Arrange
        // Act
        // Assert: Verifica erro 400
    }
}


public class RankingControllerTests
{
    [Fact]
    public async Task Deve_Visualizar_Ranking_Geral_Turma()
    {
        // Arrange
        // Act: GET /ranking/class/{id}
        // Assert: Verifica ordenação por média + comportamento
    }

    [Fact]
    public async Task Deve_Visualizar_Ranking_Por_Disciplina()
    {
        // Arrange
        // Act: GET /ranking/subject/{id}
        // Assert: Verifica ordenação correta
    }
}
