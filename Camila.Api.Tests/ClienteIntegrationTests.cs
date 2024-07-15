using System.Net.Http.Json;

using Camila.Api.Data;
using Camila.Api.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Camila.Api.Tests;
public class ClienteIntegrationTests :  IClassFixture<WebApplicationFactory<Program>>
{
        private readonly WebApplicationFactory<Program> _factory;

        public ClienteIntegrationTests(WebApplicationFactory<Program> factory)
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
    
        [Fact]
        public async Task Get_Clientes_Conta_Existente_Deve_Retornar_Conta()
        {
            // Arrange
            var cliente = new CriaCliente("Camila Marinho", 54321, 1000M);
            var client = _factory.CreateClient();

            // Act

            await client.PostAsJsonAsync("/v1.0/clientes", cliente);

            var response = await client.GetFromJsonAsync<ClienteSummary>($"/v1.0/clientes/{cliente.NumeroConta}");

            // Assert                
            Assert.Equal(cliente.Nome, response.Nome);
            Assert.Equal(cliente.NumeroConta, response.NumeroConta);
            Assert.Equal(cliente.Saldo, response.Saldo);
        }
    
    [Fact]
    public async Task Post_Clientes_Deve_Criar_Cliente()
    {
        // Arrange
        var cliente = new CriaCliente("Camila Marinho", 135246, 1000M);

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

    [Fact]
    public async Task Get_Cliente_Deve_Retornar_Not_Found()
    {
        // Arrange
        var client = _factory.CreateClient();

        //Para validar este teste devemos aguardar os demais testes executarem
        await Task.Delay(500);

        var provider = _factory.Services.GetRequiredService<IServiceProvider>();
        var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CamilaContext>();
        context.Clientes.RemoveRange(context.Clientes);
        context.SaveChanges();

        // Act
        var response = await client.GetFromJsonAsync<IEnumerable<ClienteSummary>>("/v1.0/clientes");

        // Assert                
        Assert.Empty(response);
    }
}