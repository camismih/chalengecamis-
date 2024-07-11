using Camila.Api.Models;

namespace Camila.Api.Data;

public interface ITransferenciaRepository
{
    Task RealizarTransferenciaAsync(Cliente contaOrigem, Cliente contaDestino, decimal valor, bool sucesso);
    Task<IEnumerable<TransferenciaSummary>> SelecionarTransferenciaPorNumeroContaAsync(int numeroConta);
}