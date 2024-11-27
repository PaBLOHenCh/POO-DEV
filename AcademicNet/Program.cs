using Microsoft.EntityFrameworkCore;
using AcademicNet.Data;
using Microsoft.AspNetCore.Identity;
using AcademicNet.Interfaces;
using AcademicNet.Services;
using AcademicNet.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AcademicNetDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
/*//adicionando o serviço que controla as permissoes e usuarios
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AcademicNetDbContext>()
    .AddDefaultTokenProviders();*/

builder.Services.AddControllers();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudiesGroupService, StudiesGroupService>();
builder.Services.AddScoped<IStudiesGroupRepository, StudiesGroupRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();

app.UseRouting();
app.MapControllers();


app.Run();