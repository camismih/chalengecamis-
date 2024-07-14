namespace Camila.Api.Data;

public class Cliente
{
    public Guid Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public int NumeroConta { get; init; }
    public decimal Saldo { get; set; }
        

    public void Depositar(decimal valor) => Saldo += valor;
    public void Sacar(decimal valor) => Saldo -= valor;
}