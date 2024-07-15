using Microsoft.AspNetCore.Mvc.Localization;

namespace Camila.Api.Data;

/// <summary>
/// Define os dados de um cliente e sua conta
/// </summary>
public class Cliente
{
    /// <summary>
    /// Identificador �nico do cliente
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Nome do cliente
    /// </summary>
    required public string Nome { get; init; } = string.Empty;
    
    /// <summary>
    /// N�mero da Conta do cliente
    /// </summary>
    required public int NumeroConta { get; init; }
    
    /// <summary>
    /// Saldo da conta do cliente
    /// </summary>
    public decimal Saldo { get; private set; }
    
    /// <summary>
    /// Vers�o do registro no banco de dados. Utilizado para concorr�ncia
    /// </summary>
    public int Versao { get; private set; } = 0;

    /// <summary>
    /// Deposita um valor na conta do cliente
    /// </summary>
    /// <param name="valor"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Depositar(decimal valor)
    {
        if (valor <= 0)
                {
            throw new ArgumentException($"{valor:c} n�o � um valor v�lido para um dep�sito");
        }
        
        Saldo += valor;
        Versao++;
    }

    /// <summary>
    /// Retira um valor da conta do cliente
    /// </summary>
    /// <param name="valor"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void Sacar(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ArgumentException($"{valor:c} n�o � um valor v�lido para um saque.");
        }

        if (valor > Saldo)
        {
            throw new InvalidOperationException("Saldo insuficiente para opera��o.");
        }

        Saldo -= valor;
        Versao++;
    }

    /// <summary>
    /// Constr�i uma nova inst�ncia de Cliente
    /// </summary>
    /// <param name="nome"></param>
    /// <param name="numeroConta"></param>
    /// <param name="saldo"></param>
    /// <returns></returns>
    public static Cliente Create(string nome, int numeroConta, decimal saldo) => new Cliente()
    {
        Nome = nome,
        NumeroConta = numeroConta,
        Saldo = saldo
    };
}