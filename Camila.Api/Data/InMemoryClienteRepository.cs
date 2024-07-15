

using Camila.Api.Models;

namespace Camila.Api.Data;

public class InMemoryClienteRepository : IClienteRepository
{
    private readonly List<Cliente> _clientes = [];
    
    public Task<ClienteSummary> CriarClienteAsync(CriaCliente request)
    {
        var cliente = Cliente.Create(request.Nome, request.NumeroConta, request.Saldo);

        _clientes.Add(cliente);

        return Task.FromResult(new ClienteSummary(cliente.Id, cliente.Nome, cliente.NumeroConta, cliente.Saldo, cliente.Versao));
    }

    public Task<Resultado> DepositarAsync(ClienteSummary conta, decimal valor)
    {
        var currentConta = _clientes.FirstOrDefault(c => c.Id == conta.Id && c.Versao == conta.Versao);

        if (currentConta is null)
        {
            return Task.FromResult(Resultado.Falha("Inconsistência de dados. Favor tentar novamente."));
        }

        currentConta.Depositar(valor);

        return Task.FromResult(Resultado.Sucesso());
    }

    public Task<Resultado> SacarAsync(ClienteSummary conta, decimal valor)
    {
        var currentConta = _clientes.FirstOrDefault(c => c.Id == conta.Id && c.Versao == conta.Versao);

        if (currentConta is null)
        {
            return Task.FromResult(Resultado.Falha("Inconsistência de dados. Favor tentar novamente."));
        }

        currentConta.Sacar(valor);

        return Task.FromResult(Resultado.Sucesso());
    }

    public Task<Cliente> SelecionarClientePorId(Guid id)
    {
        return Task.FromResult(_clientes.FirstOrDefault(cc => cc.Id == id));
    }

    public Task<ClienteSummary> SelecionarClientePorNumeroConta(int numeroConta)
    {
        var cliente = _clientes.FirstOrDefault(c => c.NumeroConta == numeroConta);

        if (cliente is null)
        {
            return Task.FromResult<ClienteSummary>(default!);
        }

        return Task.FromResult(new ClienteSummary(cliente.Id, cliente.Nome, cliente.NumeroConta, cliente.Saldo, cliente.Versao));
    }

    public Task<IEnumerable<ClienteSummary>> SelecionarTodosClientes() => Task.FromResult(_clientes.Select(c => new ClienteSummary(c.Id, c.Nome, c.NumeroConta, c.Saldo, c.Versao)));


    public Task<bool> VerificaContaExisteAsync(int numeroConta) => Task.FromResult(_clientes.Any(c => c.NumeroConta == numeroConta));
}
