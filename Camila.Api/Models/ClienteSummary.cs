namespace Camila.Api.Models;

public record  class  ClienteSummary(Guid Id, string Nome, int NumeroConta, decimal Saldo, int Versao);