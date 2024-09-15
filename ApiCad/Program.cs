

using UserModule.Application.Services;
using UserModule.Domain.Ports;
using UserModule.Domain.Services;
using UserModule.Infrastructure;
using UserModule.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adicionar configuração do MongoDB
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configurar MongoDB
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection");
var mongoDatabaseName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");
builder.Services.AddSingleton<IMongoContext>(new MongoContext(mongoConnectionString, mongoDatabaseName));

// Registrar repositórios e serviços
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configurar os serviços da aplicação
builder.Services.AddScoped<IUserService, UserService>();


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
