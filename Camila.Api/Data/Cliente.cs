using Microsoft.AspNetCore.Mvc.Localization;

namespace Camila.Api.Data;

public class Cliente
{
    public Guid Id { get; init; }
    required public string Nome { get; init; } = string.Empty;
    required public int NumeroConta { get; init; }
    public decimal Saldo { get; private set; }
    public int Versao { get; private set; } = 0;

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
                {
            throw new ArgumentException($"{valor:c} não é um valor válido para um depósito");
        }
        
        Saldo += valor;
        Versao++;
    }

    public void Sacar(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentException($"{valor:c} não é um valor válido para um saque.");
        }

        if (valor > Saldo)
        {
            throw new InvalidOperationException("Saldo insuficiente para operação.");
        }

        Saldo -= valor;
        Versao++;
    }

    public static Cliente Create(string nome, int numeroConta, decimal saldo) => new Cliente()
    {
        Nome = nome,
        NumeroConta = numeroConta,
        Saldo = saldo
    };
}