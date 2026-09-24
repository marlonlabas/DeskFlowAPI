using DeskFlowAPI.Models.Entidades;

namespace DeskFlowAPI.Repositories.Interfaces;
public interface ICategoriasInterface
{
    Task<List<Categoria>> ObterTodasAsync();
    Task<Categoria?> ObterPorIdAsync(int id);
    Task InserirAsync(Categoria categoria);
    Task AtualizarAsync(Categoria categoria);
    Task DeletarAsync(int id);
    Task<bool> ExisteChamadoVinculadoAsync (int categoriaId);
}