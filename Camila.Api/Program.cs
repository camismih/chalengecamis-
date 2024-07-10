using Camila.Api.Data;
using Camila.Api.Models;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

List<Cliente> clientes = [];

app.MapPost("/v1.0/clientes", ([FromBody]CriaCliente request) => 
{
    var cliente = new Cliente(request.Nome, request.NumeroConta, request.Saldo);
    clientes.Add(cliente);
return Results.Ok(cliente);
});

app.Run();