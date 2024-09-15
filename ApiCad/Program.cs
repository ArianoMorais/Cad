using UserModule.Application.Services;
using UserModule.Application.Application.Extensions;
using UserModule.Domain.Ports;
using UserModule.Domain.Services;
using UserModule.Configuration.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Adicionar configuração do MongoDB
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Registrar serviços do módulo de usuário
builder.Services.AddRepositorys(builder.Configuration);

// Configurar os serviços da aplicação
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
