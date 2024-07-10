namespace Camila.Api.Data;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public int NumeroConta { get; private set; }
    public decimal Saldo { get; private set; }

    public Cliente(string nome, int numeroConta, decimal saldo)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        NumeroConta = numeroConta;
        Saldo = saldo;
    }
}