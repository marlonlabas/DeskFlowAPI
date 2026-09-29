using DeskFlowAPI.Models.Entidades;

namespace DeskFlowAPI.Services.Interfaces;
public interface IChamadosService
{
    Task <Chamado> CriarAsync(Chamado chamado);
    Task <Chamado> ObterPorIdAsync(int id);
    Task <List<Chamado>> ObterComFiltrosAsync(Status? status, Prioridade? prioridade, int? categoriaId);
    Task AtualizarAsync(int id, string novoTitulo, string? novaDescricao, Prioridade? novaPrioridade, string? novoSolicitanteNome, int? novaCategoriaId);
    Task DeletarAsync (int id);
    Task IniciarAtendimentoAsync(int id);
    Task EncerrarAtendimentoAsync(int id, string solucao);
    Task<Interacao> AdicionarInteracaoAsync(int id, string autor, string mensagem);
}