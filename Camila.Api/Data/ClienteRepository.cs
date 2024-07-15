
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

    public async Task<Resultado> DepositarAsync(ClienteSummary conta, decimal valor)
    {
        var currentConta = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == conta.Id && c.Versao == conta.Versao);

        if (currentConta is null)
        {
            return Resultado.Falha("Inconsistência de dados. Favor tentar novamente.");
        }

        currentConta.Depositar(valor);
        await _context.SaveChangesAsync();

        return Resultado.Sucesso();
    }

    public async Task<Resultado<ClienteSummary>> CriarClienteAsync(CriaCliente request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
        {
            return Resultado<ClienteSummary>.Falha("Nome é obrigatório.");
        }

        if (request.NumeroConta <= 0)
        {
            return Resultado<ClienteSummary>.Falha("Número da Conta deve ser um número inteiro maior que zero.");
        }
                
        if (await VerificaContaExisteAsync(request.NumeroConta))
        {
            return Resultado<ClienteSummary>.Falha ("Número de conta já existe.");
        }
        
        var cliente = Cliente.Create(request.Nome, request.NumeroConta, request.Saldo);

        _context.Clientes.Add(cliente);

        await _context.SaveChangesAsync();

        return Resultado<ClienteSummary>.Sucesso(new ClienteSummary(cliente.Id, cliente.Nome, cliente.NumeroConta, cliente.Saldo, cliente.Versao));
    }

    public async Task<ClienteSummary> SelecionarClientePorNumeroConta(int numeroConta)
    {
        return await _context.Clientes
            .AsNoTracking()
            .Select(c => new ClienteSummary(c.Id, c.Nome, c.NumeroConta, c.Saldo, c.Versao))
            .FirstOrDefaultAsync(c => c.NumeroConta == numeroConta);
    }

    public async Task<IEnumerable<ClienteSummary>> SelecionarTodosClientes()
    {
        return await _context.Clientes.Select(c => new ClienteSummary(c.Id, c.Nome, c.NumeroConta, c.Saldo, c.Versao)).ToListAsync();
    }

    public async Task<bool> VerificaContaExisteAsync(int numeroConta)
    {
        return await _context.Clientes
            .AsNoTracking()
            .AnyAsync(c => c.NumeroConta == numeroConta);
    }

    public async Task<Resultado> SacarAsync(ClienteSummary conta, decimal valor)
    {
        var currentConta = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == conta.Id && c.Versao == conta.Versao);

        if (currentConta is null)
        {
            return Resultado.Falha("Inconsistência de dados. Favor tentar novamente.");
        }

        currentConta.Sacar(valor);
        await _context.SaveChangesAsync();

        return Resultado.Sucesso();
    }

    public async Task<Cliente> SelecionarClientePorId(Guid id)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
    }
}
