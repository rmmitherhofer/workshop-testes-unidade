namespace demo.cap.Interfaces.Repositories;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> ObterTodos();
    Task<bool> Adicionar(TEntity entity);
    Task<bool> Atualizar(TEntity entity);
    Task<bool> Remover(TEntity entity);
}
