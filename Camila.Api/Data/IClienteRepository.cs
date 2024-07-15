using Camila.Api.Models;

namespace Camila.Api.Data;

/// <summary>
/// Define um repositório para manipulação de dados de clientes
/// </summary>
public interface IClienteRepository
{
    /// <summary>
    /// Retorna todos os clientes registrados
    /// </summary>
    /// <returns>Retorna uma lista de <c>ClienteSummary</c></returns>
    Task<IEnumerable<ClienteSummary>> SelecionarTodosClientes();

    /// <summary>
    /// Seleciona  um cliente pelo seu número de conta
    /// </summary>
    /// <param name="numeroConta"></param>
    /// <returns>Retorna um <c>ClienteSummary</c> com os dados do cliente</returns>
    Task<ClienteSummary> SelecionarClientePorNumeroConta(int numeroConta);
    
    /// <summary>
    /// Seleciona um cliente pelo seu identificador único
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna um <c>ClienteSummary</c> com os dados do cliente</returns>
    Task<Cliente> SelecionarClientePorId(Guid id);

    /// <summary>
    /// Verifica se uma conta existe
    /// </summary>
    /// <param name="numeroConta"></param>
    /// <returns>Retorna verdadeiro se uma conta existe ou falso em caso contrário</returns>
    Task<bool> VerificaContaExisteAsync(int numeroConta);

    /// <summary>
    /// Registra um novo cliente
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Retorna um <c>Resultado</c> indicando se a operaçao foi concluída com sucesso contendo um <c>ClienteSummary</c> com os dadsos do novo cliente</returns>
    Task<Resultado<ClienteSummary>> CriarClienteAsync(CriaCliente request);

    /// <summary>
    /// Deposita um valor na conta de um cliente
    /// </summary>
    /// <param name="conta"></param>
    /// <param name="valor"></param>
    /// <returns>Reotrna um <c>Resultado</c> indicando o sucesso da operação</returns>
    Task<Resultado> DepositarAsync(ClienteSummary conta, decimal valor);

    /// <summary>
    /// Retira um valor da conta de um cliente
    /// </summary>
    /// <param name="conta"></param>
    /// <param name="valor"></param>
    /// <returns>Reotrna um <c>Resultado</c> indicando o sucesso da operação</returns>
    Task<Resultado> SacarAsync(ClienteSummary conta, decimal valor);
}
