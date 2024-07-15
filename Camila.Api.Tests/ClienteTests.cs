using System.Net.Http.Json;

using Camila.Api.Data;
using Camila.Api.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Camila.Api.Tests;

    public class EmptyTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public EmptyTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Cliente_Deve_Retornar_Not_Found()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetFromJsonAsync<IEnumerable<ClienteSummary>>("/v1.0/clientes");

            // Assert                
            Assert.Empty(response);
        }
    }

    public class NotFoundTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public NotFoundTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Clientes_Conta_Inexistente_Deve_Retornar_Nt_Found()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/v1.0/clientes/99999");

            // Assert                
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }      
    }

    public class GetClienteTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetClienteTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Clientes_Conta_Existente_Deve_Retornar_Conta()
        {
            // Arrange
            var cliente = new CriaCliente()
            {
                Nome = "Camila Marinho",
                NumeroConta = 54321,
                Saldo = 1000M
            };
            var client = _factory.CreateClient();

            // Act

            await client.PostAsJsonAsync("/v1.0/clientes", cliente);

            var response = await client.GetFromJsonAsync<ClienteSummary>($"/v1.0/clientes/{cliente.NumeroConta}");

            // Assert                
            Assert.Equal(cliente.Nome, response.Nome);
            Assert.Equal(cliente.NumeroConta, response.NumeroConta);
            Assert.Equal(cliente.Saldo, response.Saldo);
        }
    }

    public class CriaClienteTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CriaClienteTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

    [Fact]
    public async Task Post_Clientes_Deve_Criar_Cliente()
    {
        // Arrange
        var cliente = new CriaCliente()
        {
            Nome = "Camila Marinho",
            NumeroConta = 135246,
            Saldo = 1000M
        };

        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsJsonAsync("/v1.0/clientes", cliente);

        // Assert        
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ClienteSummary>();

        Assert.Equal(result.Nome, cliente.Nome);
        Assert.Equal(result.NumeroConta, cliente.NumeroConta);
        Assert.Equal(result.Saldo, cliente.Saldo);
    }
}