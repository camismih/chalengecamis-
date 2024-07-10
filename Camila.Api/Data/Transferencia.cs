namespace Camila.Api.Data;

public record class  Transferencia(DateTime Data, Cliente ContaOrigem, Cliente ContaDestino, decimal Valor, bool Sucesso);