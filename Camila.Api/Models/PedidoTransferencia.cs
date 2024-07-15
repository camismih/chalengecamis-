namespace Camila.Api.Models;

/// <summary>
/// Define uma requisiçao de um pedido de transferência entre contas
/// </summary>
/// <param name="NumeroContaOrigem"></param>
/// <param name="NumeroContaDestino"></param>
/// <param name="Valor"></param>
public record class PedidoTransferencia(int NumeroContaOrigem, int NumeroContaDestino, decimal Valor);