namespace demo.cap.Interfaces.Services;

public interface IClienteService<T>
{
    Task<IEnumerable<T>> ObterTodos();
    Task Adicionar(T cliente);
    Task Atualizar(T cliente);
    Task Inativar(string numero);
    Task Remover(string numero);
}