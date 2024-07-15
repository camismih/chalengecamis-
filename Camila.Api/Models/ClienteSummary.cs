namespace Camila.Api.Models;

/// <summary>
/// Define a resposta da requisiçao por dados de um ou mais clientes/contas
/// </summary>
/// <param name="Id"></param>
/// <param name="Nome"></param>
/// <param name="NumeroConta"></param>
/// <param name="Saldo"></param>
/// <param name="Versao"></param>
public record  class  ClienteSummary(Guid Id, string Nome, int NumeroConta, decimal Saldo, int Versao);