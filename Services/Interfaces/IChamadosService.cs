using DeskFlowAPI.Models.Entidades;

namespace DeskFlowAPI.Services.Interfaces;
public interface IChamadosService
{
    Task <Chamado> CriarAsync(Chamado chamado);
    Task <Chamado> ObterPorIdAsync(int id);
    Task <List<Chamado>> ObterComFiltrosAsync(Status? status, Prioridade? prioridade, int? categoriaId);
    Task AtualizarAsync(int id, string novoTitulo);
    Task DeletarAsync (int id);
}