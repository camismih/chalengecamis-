using System.Transactions;

using Camila.Api.Models;

namespace Camila.Api.Data;

/// <summary>
/// Serviço para realização de transferência entre contas
/// </summary>
public class TransferenciaService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly ITransferenciaRepository _transferenciaRepository;

    public TransferenciaService(IClienteRepository clienteRepository, ITransferenciaRepository transferenciaRepository)
    {
        _clienteRepository = clienteRepository;
        _transferenciaRepository = transferenciaRepository;
    }

    public async Task<Resultado> RealizarTransferenciaAsync(PedidoTransferencia request)
    {
        if (request.Valor > 1000M)
        {
            return Resultado.Falha("As transferências devem ser de no máximo R$ 1000,00.");
        }

        if (request.NumeroContaOrigem == request.NumeroContaDestino)
        {
            return Resultado.Falha("A conta de origem e a conta de destino devem ser diferentes.");
        }

        if (!await _clienteRepository.VerificaContaExisteAsync(request.NumeroContaOrigem))
        {
            return Resultado.Falha("Conta de origem não encontrada.");
        }

        if (!await _clienteRepository.VerificaContaExisteAsync(request.NumeroContaDestino))
        {
            return Resultado.Falha("Conta de Destino não encontrada.");
        }

        var contaOrigem = await _clienteRepository.SelecionarClientePorNumeroConta(request.NumeroContaOrigem);
        var contaDestino = await _clienteRepository.SelecionarClientePorNumeroConta(request.NumeroContaDestino);

        bool sucesso = contaOrigem.Saldo >= request.Valor;

        var currentOrigem = await _clienteRepository.SelecionarClientePorId(contaOrigem.Id);
        var currentDestino = await _clienteRepository.SelecionarClientePorId(contaDestino.Id);
           

        if (sucesso)
        {
            using (var tx = new TransactionScope())
            {
                var resultadoSaque = await _clienteRepository.SacarAsync(contaOrigem, request.Valor);
                if (!resultadoSaque.ComSucesso)
                {
                    return resultadoSaque;
                }
                var resultadoDeposito = await _clienteRepository.DepositarAsync(contaDestino, request.Valor);
                if (!resultadoDeposito.ComSucesso)
                {
                    return resultadoDeposito;
                }

                tx.Complete();
            }
        }

        await _transferenciaRepository.RealizarTransferenciaAsync(currentOrigem, currentDestino, request.Valor, sucesso);

        return Resultado.Sucesso();
    }
}