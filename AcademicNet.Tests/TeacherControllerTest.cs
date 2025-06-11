using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Controllers;
using System.Text.Json;
using System.Text;
using System.Net;
using AcademicNet.Models;

public class TeacherControllerTests
{

    private readonly HttpClient _client;

    public TeacherControllerTests()
    {
        _client = new CustomWebApplicationFactory().CreateClient();
    }
    [Fact]
    public async Task Deve_Retornar_404_Se_Professor_Nao_Existe()
    {
        // Arrange
        int professorIdInexistente = 99999;

        // Act
        var response = await _client.GetAsync($"/api/teacher/getClassSubject_byTeacherId?id={professorIdInexistente}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Deve_Retornar_400_Se_Parametros_De_Matriculas_Forem_Nulos()
    {
        // Arrange
        // Não passamos classId nem subjectId

        // Act
        var response = await _client.GetAsync("/api/teacher/getMatriculations_byClassSubjectId");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Deve_Retornar_400_Se_Nota_For_Invalida()
    {
        // Arrange
        var requestBody = new
        {
            studentId = 1,
            subjectId = 2,
            grade = -5.0f, // Nota inválida
            frequency = 80.0f
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/teacher/ThrowGrade", content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


}