namespace Camila.Api.Data;

/// <summary>
/// Define os dados de uma transferência e o sucesso da operação
/// </summary>
/// <param name="Data"></param>
/// <param name="ContaOrigem"></param>
/// <param name="ContaDestino"></param>
/// <param name="Valor"></param>
/// <param name="Sucesso"></param>
public record class Transferencia
{
    public DateTime Data { get; init; }
    public Cliente ContaOrigem { get; init; }
    public Cliente ContaDestino { get; init; }
    public decimal Valor { get; init; }
    public bool Sucesso { get; init; }

    private Transferencia() { }

    public Transferencia(DateTime data, Cliente contaOrigem, Cliente contaDestino, decimal valor, bool sucesso)
    {
        Data = data;
        ContaOrigem = contaOrigem;
        ContaDestino = contaDestino;
        Valor = valor;
        Sucesso = sucesso;
    }
}