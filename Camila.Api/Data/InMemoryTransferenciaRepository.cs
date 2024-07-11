
using Camila.Api.Models;

namespace Camila.Api.Data;

public class InMemoryTransferenciaRepository : ITransferenciaRepository
{
    private readonly List<Transferencia> _transferencias = [];

    public Task RealizarTransferenciaAsync(Cliente contaOrigem, Cliente contaDestino, decimal valor, bool sucesso)
    {
        _transferencias.Add(new Transferencia(DateTime.UtcNow, contaOrigem, contaDestino, valor, sucesso));
        return Task.CompletedTask;
    }

    public Task<IEnumerable<TransferenciaSummary>> SelecionarTransferenciaPorNumeroContaAsync(int numeroConta)
    {
        return Task.FromResult(_transferencias.Where(t => t.ContaOrigem.NumeroConta == numeroConta || (t.ContaDestino.NumeroConta == numeroConta && t.Sucesso))
            .Select(t => new TransferenciaSummary(t.Data, t.ContaOrigem.NumeroConta, t.ContaDestino.NumeroConta, t.Valor, t.Sucesso)));
    }
}
