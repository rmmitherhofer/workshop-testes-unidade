using demo.cap.Models;

namespace demo.cap.Interfaces.Repositories;

public interface IClientePfRepository : IRepository<ClientePessoaFisica>
{
    Task<ClientePessoaFisica> ObterPorCpf(string cpf);
}
