using DeskFlowAPI.Models.Entidades;

namespace DeskFlowAPI.Services.Interfaces;
public interface IChamadosServices
{
    Task <Chamado> CriarAsync(Chamado chamado);
    Task <Chamado?> ObterPorIdAsync(int id);
    Task <List<Chamado>> ObterComFiltrosAsync(Status? status, Prioridade? prioridade, int? categoriaId);
    Task <Chamado> AtualizarAsync(Chamado chamado);
}