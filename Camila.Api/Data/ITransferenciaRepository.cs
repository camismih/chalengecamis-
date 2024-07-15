using Camila.Api.Models;

namespace Camila.Api.Data;

/// <summary>
/// Define os métodos de um repositório de transferência.
/// </summary>
public interface ITransferenciaRepository
{
    /// <summary>
    /// Registra a transferência entre contas
    /// </summary>
    /// <param name="contaOrigem"></param>
    /// <param name="contaDestino"></param>
    /// <param name="valor"></param>
    /// <param name="sucesso"></param>
    /// <returns>Retorna o <c>Resultado</c> da operação.</returns>
    Task RealizarTransferenciaAsync(Cliente contaOrigem, Cliente contaDestino, decimal valor, bool sucesso);

    /// <summary>
    /// Seleciona as transferência de uma conta
    /// </summary>
    /// <param name="numeroConta"></param>
    /// <returns>Retorna uma lista de <c>TransferenciaSummary</c com os dados das transferências></returns>
    Task<IEnumerable<TransferenciaSummary>> SelecionarTransferenciaPorNumeroContaAsync(int numeroConta);
}