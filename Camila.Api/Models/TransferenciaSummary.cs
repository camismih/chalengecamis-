namespace Camila.Api.Models;

/// <summary>
/// Define a resposta da requisiçao por dados de uma ou mais transferências
/// </summary>
/// <param name="Data"></param>
/// <param name="NumeroContaOrigem"></param>
/// <param name="NumeroContaDestino"></param>
/// <param name="Valor"></param>
/// <param name="Sucesso"></param>
public record class TransferenciaSummary(DateTime Data, int NumeroContaOrigem, int NumeroContaDestino, decimal Valor, bool Sucesso);