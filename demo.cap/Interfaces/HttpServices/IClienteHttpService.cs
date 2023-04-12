using demo.cap.Models;

namespace demo.cap.Interfaces.HttpServices;
public interface IClienteHttpService
{
    Task<ClientePessoaFisica> ObterPF(string cpf);
    Task<IEnumerable<ClientePessoaFisica>> ObterTodosPF();
    Task<ClientePessoaJuridica> ObterPJ(string cnpj);
    Task<IEnumerable<ClientePessoaJuridica>> ObterTodosPJ();
}
