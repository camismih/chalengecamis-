using Camila.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace Camila.Api.Data;

public class ClienteRepository : IClienteRepository
{
    private readonly CamilaContext _context;

    public ClienteRepository(CamilaContext context)
    {
        _context = context;
    }

    public async Task AtualizarContaAsync(Cliente conta)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<ClienteSummary> CriarClienteAsync(CriaCliente request)
    {
        var cliente = Cliente.Create(request.Nome, request.NumeroConta, request.Saldo);

        _context.Clientes.Add(cliente);

        await _context.SaveChangesAsync();

        return new ClienteSummary(cliente.Id, cliente.Nome, cliente.NumeroConta, cliente.Saldo);
    }

    public async Task<Cliente> SelecionarClientePorNumeroConta(int numeroConta)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);
    }

    public async Task<IEnumerable<ClienteSummary>> SelecionarTodosClientes()
    {
        return await _context.Clientes.Select(c => new ClienteSummary(c.Id, c.Nome, c.NumeroConta, c.Saldo)).ToListAsync();
    }

    public async Task<bool> VerificaContaExisteAsync(int numeroConta)
    {
        return await _context.Clientes.AnyAsync(c => c.NumeroConta == numeroConta);
    }
}
