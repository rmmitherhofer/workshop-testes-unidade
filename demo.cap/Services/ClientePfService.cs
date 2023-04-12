using demo.cap.Interfaces.HttpServices;
using demo.cap.Interfaces.Repositories;
using demo.cap.Interfaces.Services;
using demo.cap.Models;
using demo.cap.ValuesObjects;

namespace demo.cap.Services;
public class ClientePfService : IClientePfService
{
    private readonly IClienteHttpService _clienteHttpService;
    private readonly IClientePfRepository _clientePfRepository;

    public ClientePfService(IClienteHttpService clienteHttpService, IClientePfRepository clientePfRepository)
    {
        _clienteHttpService = clienteHttpService;
        _clientePfRepository = clientePfRepository;
    }
    public async Task<IEnumerable<ClientePessoaFisica>> ObterTodos()
        => await _clienteHttpService.ObterTodosPF();

    public async Task Adicionar(ClientePessoaFisica cliente)
    {
        if (cliente.IsInvalid()) return;

        var clienteExistente = await _clienteHttpService.ObterPF(cliente.Cpf);

        if (clienteExistente is not null) throw new ArgumentException("Cliente já cadastrado.");

        cliente.Ativar();

        if (!await _clientePfRepository.Adicionar(cliente))
            throw new ApplicationException($"Falha ao adicionar cliente {cliente.NomeCompleto()}.");
    }

    public async Task Atualizar(ClientePessoaFisica cliente)
    {
        if (cliente.IsInvalid()) return;

        var clienteExistente = await ObterClienteExistente(cliente.Cpf);

        clienteExistente.Atualizar(cliente.Email, cliente.ValorPatrimonio);

        if (!await _clientePfRepository.Atualizar(cliente))
            throw new ApplicationException($"Falha ao atualizar cliente {cliente.NomeCompleto()}.");
    }

    public async Task Inativar(string cpf)
    {
        cpf = new Cpf(cpf).Numero;

        var cliente = await ObterClienteExistente(cpf);

        cliente.Inativar();

        if (!await _clientePfRepository.Atualizar(cliente))
            throw new ApplicationException($"Falha ao inativar cliente {cliente.NomeCompleto()}.");
    }

    public async Task Remover(string cpf)
    {
        cpf = new Cpf(cpf).Numero;

        var cliente = await ObterClienteExistente(cpf);

        if (!await _clientePfRepository.Remover(cliente))
            throw new ApplicationException($"Falha ao remover cliente {cliente.NomeCompleto()}.");
    }

    private async Task<ClientePessoaFisica> ObterClienteExistente(string cpf)
        => await _clienteHttpService.ObterPF(cpf) ?? throw new ArgumentException("Cliente não localizado.");
}
