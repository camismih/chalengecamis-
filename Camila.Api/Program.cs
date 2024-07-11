using Camila.Api.Data;
using Camila.Api.Models;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IClienteRepository, InMemoryClienteRepository>();
builder.Services.AddSingleton<ITransferenciaRepository, InMemoryTransferenciaRepository>();

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

app.MapPost("/v1.0/transferencias", async ([FromBody] PedidoTransferencia request, IClienteRepository clienteRepository, ITransferenciaRepository transferenciaRepository) =>
{
    if (request.Valor > 1000M)
    {
        return Results.BadRequest();
    }

    if (! await clienteRepository.VerificaContaExisteAsync(request.NumeroContaOrigem))
    {
        return Results.NotFound();
    }

    if (!await clienteRepository.VerificaContaExisteAsync(request.NumeroContaDestino))
    {
        return Results.NotFound();
    }

    var contaOrigem = await clienteRepository.SelecionarClientePorNumeroConta(request.NumeroContaOrigem);
    var contaDestino = await clienteRepository.SelecionarClientePorNumeroConta(request.NumeroContaDestino);
    
    if (contaOrigem.Saldo < request.Valor)
    {
        await transferenciaRepository.RealizarTransferenciaAsync(contaOrigem, contaDestino, request.Valor, false);
        return Results.BadRequest();
    }
    await transferenciaRepository.RealizarTransferenciaAsync(contaOrigem, contaDestino, request.Valor, true);

    contaOrigem.Sacar(request.Valor);
    contaDestino.Depositar(request.Valor);

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