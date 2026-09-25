using DeskFlowAPI.Models.Entidades;

namespace DeskFlowAPI.Services.Interfaces;

public interface ICategoriasService
{
    Task<List<Categoria>> ObterTodasAsync();
    Task<Categoria> ObterPorIdAsync(int id);
    Task <Categoria> CriarAsync(Categoria categoria);
    Task AtualizarAsync(int id, string novoNome);
    Task DeletarAsync(int id);
}