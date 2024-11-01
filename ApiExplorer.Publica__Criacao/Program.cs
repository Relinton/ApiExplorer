using ApiExplorer.Publica__Criacao.Models.Interfaces;
using ApiExplorer.Publica__Criacao.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IReservaRepository, ReservaRepository>();

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
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
