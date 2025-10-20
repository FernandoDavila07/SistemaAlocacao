using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Extensions; // <-- LINHA FALTANDO

var builder = WebApplication.CreateBuilder(args);

// --- 1. Defini��o da Pol�tica de CORS ---
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// --- 2. Adi��o dos Servi�os ---
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 3. Configurar o DbContext (CORRIGIDO) ---

// Pega as vari�veis de ambiente do Kubernetes
var host = Environment.GetEnvironmentVariable("DB_HOST");
var user = Environment.GetEnvironmentVariable("DB_USER");
var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");
var db = Environment.GetEnvironmentVariable("DB_NAME");

// Monta a connection string manualmente
var connectionString = $"Server={host};Database={db};User={user};Password={pass};";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.MigrationsAssembly("Infrastructure")
    )
);

// --- 4. Configurar a Inje��o de Depend�ncia (Reposit�rios) ---
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IGrupoVeiculosRepository, GrupoVeiculosRepository>();
builder.Services.AddScoped<IEmpresaAssistenciaRepository, EmpresaAssistenciaRepository>();
builder.Services.AddScoped<IPlanoAssistenciaRepository, PlanoAssistenciaRepository>();
builder.Services.AddScoped<IVeiculoAssistenciaRepository, VeiculoAssistenciaRepository>();

// --- 5. Constru��o do App ---
var app = builder.Build();

// --- LINHA FALTANDO --> Adiciona a chamada para criar as tabelas
app.ApplyMigrations();
// -----------------------------------------------------------------

// --- 6. Configura��o do Pipeline de Requisi��es HTTP ---
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection(); 

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();