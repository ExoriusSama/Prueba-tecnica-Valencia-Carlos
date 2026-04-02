using CineApi.Contexts;
using CineApi.Repository;
using CineApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//llamar a los servicios de pelicula
builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();
builder.Services.AddScoped<IPeliculaService, PeliculaService>();

//llamar a los servicios de sala_cine
builder.Services.AddScoped<ISalaCineRepository, SalaCineRepository>();
builder.Services.AddScoped<ISalaCineService, SalaCineService>();

//llamar a los servicios de Pelicula sala cine
builder.Services.AddScoped<IPeliculaSalaRepository, PeliculaSalaRepository>();
builder.Services.AddScoped<IPeliculaSalaService, PeliculaSalaService>();

// Configuración de la conexión a la base de datos
builder.Services.AddDbContext<CineContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder.AllowAnyOrigin() //builder.WithOrigins("http://localhost:4200") // URL de tu proyecto Angular
            .AllowAnyMethod()// Permite cualquier método HTTP (GET, POST, etc.)
            .AllowAnyHeader());// Permite cualquier encabezado
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
