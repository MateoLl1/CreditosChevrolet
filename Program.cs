using Microsoft.EntityFrameworkCore;
using CreditosChevrolet.Data;
using CreditosChevrolet.Repository.IRepository;
using CreditosChevrolet.Repository;
using CreditosChevrolet.Services.Interfaces;
using CreditosChevrolet.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ISolicitudCreditoRepository, SolicitudCreditoRepository>();
builder.Services.AddScoped<IRespuestaCreditoFinancieraRepository, RespuestaCreditoFinancieraRepository>();
builder.Services.AddScoped<INotificacionAsesorRepository, NotificacionAsesorRepository>();
builder.Services.AddScoped<ICreditoService, CreditoService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
