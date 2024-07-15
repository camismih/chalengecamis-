namespace Camila.Api.Models;

/// <summary>
/// Define a requisi��o para a cria��o de uma nova conta
/// </summary>
/// <param name="Nome"></param>
/// <param name="NumeroConta"></param>
/// <param name="Saldo"></param>
public record class CriaCliente(string Nome, int NumeroConta , decimal Saldo);