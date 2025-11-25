using Microsoft.EntityFrameworkCore;
using CreditosChevrolet.Data;
using CreditosChevrolet.Repository.IRepository;
using CreditosChevrolet.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


// Conexion a SQL SERVER
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Inyeccion de dependencias
builder.Services.AddScoped<ISolicitudCreditoRepository, SolicitudCreditoRepository>();
builder.Services.AddScoped<IRespuestaCreditoFinancieraRepository, RespuestaCreditoFinancieraRepository>();
builder.Services.AddScoped<INotificacionAsesorRepository, NotificacionAsesorRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
