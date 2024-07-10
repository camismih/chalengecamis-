using Camila.Api.Models;

namespace Camila.Api.Data;

public interface IClienteRepository
{
    Task<IEnumerable<ClienteSummary>> SelecionarTodosClientes();
    Task<Cliente> SelecionarClientePorNumeroConta(int numeroConta);

    Task<ClienteSummary> CriarClienteAsync(CriaCliente request);
}
