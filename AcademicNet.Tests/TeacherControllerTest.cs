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
    [Fact]
    public async Task Deve_Criar_Professor_Com_Disciplina()
    {
        // Arrange
        // Act: POST /teacher
        // Assert: Verifica 201 Created
    }

    [Fact]
    public async Task Deve_Editar_Professor()
    {
        // Arrange
        // Act: PUT /teacher/{id}
        // Assert: Verifica 200 OK
    }
}