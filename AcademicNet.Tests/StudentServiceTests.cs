
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcademicNet.Data;
using AcademicNet.Models;
using AcademicNet.Repositories;
using AcademicNet.Services;
using AcademicNet.DTO;
using AcademicNet.Interfaces;
using Allure.Xunit.Attributes;
using Allure.Net.Commons;

[AllureSuite("Student Service")]
[AllureOwner("Pablo")]
[AllureEpic("AcademicNet API")]
public class StudentServiceTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly AcademicNetDbContext _context;
    private readonly StudentService _studentService;

    public StudentServiceTests()
    {
        var options = new DbContextOptionsBuilder<AcademicNetDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AcademicNetDbContext(options);

        // Seed básico de dados
        _context.Students.Add(new StudentModel
        {
            Id = 1,
            Name = "Aluno de Teste",
            Email = "aluno.teste@example.com",
            Password = "SenhaForte123",
            Phone = "11999999999",
            CPF = "123.456.789-00",
            Role = IdentityRole.Student,
            Address = new AddressModel
            {
                Logradouro = "Rua das Flores",
                Numero = "123",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                CEP = "12345-678",
                Complemento = "Apto 101",
                Referencia = "Próximo à praça central"
            }
        });
        _context.SaveChanges();

        var studentRepository = new StudentRepository(_context);
        var subjectRepository = new SubjectRepository(_context);

        _studentService = new StudentService(studentRepository, subjectRepository);
    }

    // ========================
    // TESTES DE ERRO E VALIDAÇÃO
    // ========================

    [AllureFeature("Ranking per Student Invalido")]
    [AllureSeverity(SeverityLevel.normal)]
    [Fact]
    public async Task GetRanking_perStudent_Deve_Lancar_ArgumentException_Se_Filtros_Invalidos()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _studentService.GetRanking_perStudent(unitId: 1, classId: null, subjectId: null));
    }

    [AllureFeature("Ranking per Class Invalido")]
    [AllureSeverity(SeverityLevel.normal)]
    [Fact]
    public async Task GetRanking_per_Class_Deve_Lancar_ArgumentException_Se_Filtros_Invalidos()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _studentService.GetRanking_per_Class(unitId: 1, classId: 1, subjectId: 1));
    }

    [AllureFeature("Ranking per Unit Invalido")]
    [AllureSeverity(SeverityLevel.normal)]
    [Fact]
    public async Task GetRanking_perUnit_Deve_Retornar_Sucesso_Se_SubjectId_Nulo()
    {
        var result = await _studentService.GetRanking_perUnit(null);

        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    // ========================
    // TESTES DE PERFORMANCE
    // ========================

    [AllureFeature("Ranking per Student 100 Requisicoes")]
    [AllureSeverity(SeverityLevel.normal)]
    [Fact]
    public async Task Performance_GetRanking_perStudent_100_Requisicoes()
    {
        int totalRequests = 100;
        var tasks = new Task<IEnumerable<RankingDTO>>[totalRequests];

        var inicio = DateTime.Now;

        for (int i = 0; i < totalRequests; i++)
        {
            tasks[i] = _studentService.GetRanking_perStudent(unitId: null, classId: null, subjectId: null);
        }

        await Task.WhenAll(tasks);
        var duracao = DateTime.Now - inicio;

        foreach (var resultado in tasks)
        {
            Assert.NotNull(resultado.Result);
        }

        Assert.True(duracao.TotalSeconds < 10, $"Demorou {duracao.TotalSeconds} segundos.");
    }

    [AllureFeature("Ranking per Class 50 Chamadas Sequenciais")]
    [AllureSeverity(SeverityLevel.normal)]
    [Fact]
    public async Task Performance_GetRanking_per_Class_50_Chamadas_Sequenciais()
    {
        var inicio = DateTime.Now;

        for (int i = 0; i < 50; i++)
        {
            var result = await _studentService.GetRanking_per_Class(unitId: null, classId: null, subjectId: null);
            Assert.NotNull(result);
        }

        var duracao = DateTime.Now - inicio;

        Assert.True(duracao.TotalSeconds < 15, $"Demorou {duracao.TotalSeconds} segundos.");
    }
}
