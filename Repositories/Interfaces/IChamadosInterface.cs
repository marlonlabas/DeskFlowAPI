using DeskFlowAPI.Models.Entidades;

namespace DeskFlowAPI.Repositories.Interfaces;
public interface IChamadosInterface
{
    Task CriarAsync(Chamado chamado);
    Task <Chamado?> ObterPorIdAsync(int id);
    Task <List<Chamado>> ObterComFiltrosAsync(Status? status, Prioridade? prioridade, int? categoriaId);
    Task AtualizarAsync(Chamado chamado);
}