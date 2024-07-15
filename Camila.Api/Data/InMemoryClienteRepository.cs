
using Camila.Api.Models;

namespace Camila.Api.Data;

public class InMemoryClienteRepository : IClienteRepository
{
    private readonly List<Cliente> _clientes = [];

    public Task AtualizarContaAsync(Cliente conta)
    {
        return Task.CompletedTask;
    }

    public Task<ClienteSummary> CriarClienteAsync(CriaCliente request)
    {
        var cliente = Cliente.Create(request.Nome, request.NumeroConta, request.Saldo);

        _clientes.Add(cliente);

        return Task.FromResult(new ClienteSummary(cliente.Id, cliente.Nome, cliente.NumeroConta, cliente.Saldo));
    }

    public Task<Cliente> SelecionarClientePorNumeroConta(int numeroConta)
    {
        var cliente = _clientes.FirstOrDefault(c => c.NumeroConta == numeroConta);

        if (cliente is null)
        {
            return Task.FromResult<Cliente>(default!);
        }

        return Task.FromResult(cliente);
    }

    public Task<IEnumerable<ClienteSummary>> SelecionarTodosClientes() => Task.FromResult(_clientes.Select(c => new ClienteSummary(c.Id, c.Nome, c.NumeroConta, c.Saldo)));


    public Task<bool> VerificaContaExisteAsync(int numeroConta) => Task.FromResult(_clientes.Any(c => c.NumeroConta == numeroConta));
}
