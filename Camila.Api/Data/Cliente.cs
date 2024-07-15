namespace Camila.Api.Data;

public class Cliente
{
    public Guid Id { get; init; }
    required public string Nome { get; init; } = string.Empty;
    required public int NumeroConta { get; init; }
    public decimal Saldo { get; private set; }
    

    public void Depositar(decimal valor) => Saldo += valor;
    public void Sacar(decimal valor) => Saldo -= valor;

    public static Cliente Create(string nome, int numeroConta, decimal saldo) => new Cliente()
    {
        Nome = nome,
        NumeroConta = numeroConta,
        Saldo = saldo
    };
}