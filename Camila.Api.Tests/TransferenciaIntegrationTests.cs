﻿using System.Net;
using System.Net.Http.Json;

using Camila.Api.Data;
using Camila.Api.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Camila.Api.Tests;

public class TransferenciaIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TransferenciaIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_Transferencia_Deve_Realizar_Transferencia()
    {
        // Arrange
        var origem = new CriaCliente("Camila Marinho", 67890, 1000M);

        var destino = new CriaCliente("Enzo Marinho", 98765, 0M);

        var pedido = new PedidoTransferencia(origem.NumeroConta, destino.NumeroConta, 200M);
        var client = _factory.CreateClient();

        // Act
        await client.PostAsJsonAsync("/v1.0/clientes", origem);
        await client.PostAsJsonAsync("/v1.0/clientes", destino);

        var response = await client.PostAsJsonAsync("/v1.0/transferencias", pedido);

        var currentOrigem = await client.GetFromJsonAsync<ClienteSummary>($"/v1.0/clientes/{origem.NumeroConta}");
        var currentDestino = await client.GetFromJsonAsync<ClienteSummary>($"/v1.0/clientes/{destino.NumeroConta}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(origem.Saldo - 200M, currentOrigem.Saldo);
        Assert.Equal(destino.Saldo + 200M, currentDestino.Saldo);
    }

    [Fact]
    public async Task Post_Transferencia_Maior_Limite_Deve_Retornar_bad_Request()
    {
        //// Arrange
        var pedido = new PedidoTransferencia(12345, 54321, 1000000M);
        var client = _factory.CreateClient();

        // Act
        var result = await client.PostAsJsonAsync("/v1.0/transferencias", pedido);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}