namespace Camila.Api.Models;

public record class CriaCliente
{    
    required public string Nome {get; init;}
    required public int NumeroConta {get; init;}
    required public decimal Saldo {get; init;}
}