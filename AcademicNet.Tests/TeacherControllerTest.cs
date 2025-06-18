using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using AcademicNet.DTO;
using AcademicNet.Controllers;
using System.Text.Json;
using System.Text;
using System.Net;
using AcademicNet.Models;
using Allure.Xunit.Attributes;
using Allure.Net.Commons;

[AllureSuite("Teacher Controller")]
[AllureOwner("Pablo")]
[AllureEpic("AcademicNet API")]
public class TeacherControllerTests : IClassFixture<CustomWebApplicationFactory>
{

    private readonly HttpClient _client;

    public TeacherControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [AllureFeature("Not Return Teacher")]
    [AllureSeverity(SeverityLevel.normal)]
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

    [AllureFeature("Null Parameters to Matriculations")]
    [AllureSeverity(SeverityLevel.normal)]
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

    [AllureFeature("Invalid Grade")]
    [AllureSeverity(SeverityLevel.normal)]
    [Fact]
    public async Task Deve_Retornar_400_Se_Nota_For_Invalida()
    {
        // Arrange
        var url = "/api/teacher/ThrowGrade?" +
          $"studentId={1}" +
          $"&subjectId={2}" +
          $"&grade={-5.0f}" +
          $"&frequency={0.8f}";


        // Act
        var response = await _client.PostAsync(url, null);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


}