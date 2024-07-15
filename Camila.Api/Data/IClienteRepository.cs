using Camila.Api.Models;

namespace Camila.Api.Data;

public interface IClienteRepository
{
    Task<IEnumerable<ClienteSummary>> SelecionarTodosClientes();
    Task<ClienteSummary> SelecionarClientePorNumeroConta(int numeroConta);
    Task<Cliente> SelecionarClientePorId(Guid id);
    Task<bool> VerificaContaExisteAsync(int numeroConta);

    Task<ClienteSummary> CriarClienteAsync(CriaCliente request);

    Task<Resultado> DepositarAsync(ClienteSummary conta, decimal valor);
    Task<Resultado> SacarAsync(ClienteSummary conta, decimal valor);
}
