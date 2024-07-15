using Camila.Api.Data;
using Camila.Api.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<CamilaContext>(options =>
{
    options.UseInMemoryDatabase("Camila");
    options.LogTo(Console.WriteLine);
    options.EnableSensitiveDataLogging();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ITransferenciaRepository, TransferenciaRepository>();
builder.Services.AddScoped<TransferenciaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/v1.0/clientes", async ([FromBody]CriaCliente request, IClienteRepository repository) => 
{
    var cliente = await repository.CriarClienteAsync(request);
return Results.Ok(cliente);
});

app.MapGet("/v1.0/clientes", async (IClienteRepository repository) => Results.Ok(await repository.SelecionarTodosClientes()));

app.MapGet("/v1.0/clientes/{numeroConta:int}", async (int  numeroConta, IClienteRepository repository) =>
{
    var cliente = await repository.SelecionarClientePorNumeroConta(numeroConta);
    if (cliente is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(new ClienteSummary(cliente.Id, cliente.Nome, cliente.NumeroConta, cliente.Saldo));
});

app.MapPost("/v1.0/transferencias", async ([FromBody] PedidoTransferencia request, TransferenciaService service) =>
{
    var resultado = await service.RealizarTransferenciaAsync(request);

    if (!resultado.ComSucesso)
    {
        return Results.BadRequest(resultado.Erro);
    }

    return Results.Ok();
});

app.MapGet("/v1.0/transferencia/{numeroConta:int}", async ([FromRouteAttribute] int numeroConta, ITransferenciaRepository repository) =>
{
    var transferencias = await repository.SelecionarTransferenciaPorNumeroContaAsync(numeroConta);
    if (!transferencias.Any())
    {
        return Results.NotFound();
    }

    return Results.Ok(transferencias);
});

app.Run();

public partial class Program
{

}