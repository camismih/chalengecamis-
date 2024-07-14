using Camila.Api.Models;

namespace Camila.Api.Data;

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
        
    await _transferenciaRepository.RealizarTransferenciaAsync(contaOrigem, contaDestino, request.Valor, sucesso);

        if (sucesso)
        {
            contaOrigem.Sacar(request.Valor);
            contaDestino.Depositar(request.Valor);

            await _clienteRepository.AtualizarContaAsync(contaOrigem);
            await _clienteRepository.AtualizarContaAsync(contaDestino);
        }

        return Resultado.Sucesso();
    }
}