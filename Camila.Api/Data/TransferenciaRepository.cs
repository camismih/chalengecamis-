using Camila.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace Camila.Api.Data;

public class TransferenciaRepository : ITransferenciaRepository
{
    private readonly CamilaContext _context;

    public TransferenciaRepository(CamilaContext context)
    {
        _context = context;
    }

    public async Task RealizarTransferenciaAsync(Cliente contaOrigem, Cliente contaDestino, decimal valor, bool sucesso)
    {
_context.Transferencias.Add(new Transferencia(DateTime.UtcNow, contaOrigem, contaDestino, valor, sucesso));
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TransferenciaSummary>> SelecionarTransferenciaPorNumeroContaAsync(int numeroConta)
    {
        return await _context.Transferencias
            .AsNoTracking()
            .Where(t => t.ContaOrigem.NumeroConta == numeroConta || (t.ContaDestino.NumeroConta == numeroConta && t.Sucesso))
            .Select(t => new TransferenciaSummary(t.Data, t.ContaOrigem.NumeroConta, t.ContaDestino.NumeroConta, t.Valor, t.Sucesso))
            .ToListAsync();
    }
}