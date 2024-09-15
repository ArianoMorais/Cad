using UserModule.Application.Services;
using UserModule.Application.Application.Extensions;
using UserModule.Domain.Ports;
using UserModule.Domain.Services;
using UserModule.Configuration.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adicionar configura��o do MongoDB
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Registrar servi�os do m�dulo de usu�rio
builder.Services.AddRepositorys(builder.Configuration);

// Configurar os servi�os da aplica��o
builder.Services.AddServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
