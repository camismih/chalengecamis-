namespace Camila.Api.Models;

/// <summary>
/// Define a requisição para a criação de uma nova conta
/// </summary>
/// <param name="Nome"></param>
/// <param name="NumeroConta"></param>
/// <param name="Saldo"></param>
public record class CriaCliente(string Nome, int NumeroConta , decimal Saldo);