namespace Camila.Api.Data;

public class  Transferencia
{
    public DateTime Data { get; init; }
    public Cliente ContaOrigem { get; init; } = default!;
    public Cliente ContaDestino { get; init; } = default!;
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