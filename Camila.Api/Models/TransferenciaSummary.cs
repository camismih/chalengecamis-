namespace Camila.Api.Models;

public record class TransferenciaSummary(DateTime Data, int NumeroContaOrigem, int NumeroContaDestino, decimal Valor, bool Sucesso);