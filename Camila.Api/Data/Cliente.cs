using Microsoft.AspNetCore.Mvc.Localization;

namespace Camila.Api.Data;

/// <summary>
/// Define os dados de um cliente e sua conta
/// </summary>
public class Cliente
{
    /// <summary>
    /// Identificador único do cliente
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Nome do cliente
    /// </summary>
    required public string Nome { get; init; } = string.Empty;
    
    /// <summary>
    /// Número da Conta do cliente
    /// </summary>
    required public int NumeroConta { get; init; }
    
    /// <summary>
    /// Saldo da conta do cliente
    /// </summary>
    public decimal Saldo { get; private set; }
    
    /// <summary>
    /// Versão do registro no banco de dados. Utilizado para concorrência
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
            throw new ArgumentException($"{valor:c} não é um valor válido para um depósito");
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
            throw new ArgumentException($"{valor:c} não é um valor válido para um saque.");
        }

        if (valor > Saldo)
        {
            throw new InvalidOperationException("Saldo insuficiente para operação.");
        }

        Saldo -= valor;
        Versao++;
    }

    /// <summary>
    /// Constrói uma nova instância de Cliente
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