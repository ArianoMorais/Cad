using UserModule.Application.Services;
using UserModule.Application.Application.Extensions;
using UserModule.Domain.Ports;
using UserModule.Domain.Services;
using UserModule.Configuration.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicionar configura��o do MongoDB
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configurar a chave secreta para JWT

    var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = true; // Requer HTTPS em produ��o
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

// Registrar servi�os do m�dulo de usu�rio
builder.Services.AddRepositorys(builder.Configuration);

// Configurar os servi�os da aplica��o
builder.Services.AddServices();

// Configura o AutoMapper usando a classe de extens�o
builder.Services.AddCustomAutoMapper();

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

app.UseCors("AllowAllOrigins");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",
    defaults: new { action = "Index" });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
