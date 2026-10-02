using DeskFlowAPI.Exceptions;
using DeskFlowAPI.Models.Entidades;
using DeskFlowAPI.Repositories.Interfaces;
using DeskFlowAPI.Services.Interfaces;

namespace DeskFlowAPI.Services;

public class ChamadosService : IChamadosService
{
    private readonly IChamadosInterface _chamadosRepository;

    public ChamadosService(IChamadosInterface chamadosRepository)
    {
        _chamadosRepository = chamadosRepository;
    }

    public async Task<Chamado> CriarAsync(Chamado chamado)
    {
        await _chamadosRepository.CriarAsync(chamado);
        return chamado;
    }

    public async Task<Chamado> ObterPorIdAsync(int id)
    {
        var chamado = await _chamadosRepository.ObterPorIdAsync(id);
        if (chamado is null)
        {
            throw new NotFoundException($"Chamado com Id {id} não encontrado.");
        }
        return chamado;
    }

    public async Task<List<Chamado>> ObterComFiltrosAsync(Status? status, Prioridade? prioridade, int? categoriaId)
    {
        return await _chamadosRepository.ObterComFiltrosAsync(status, prioridade, categoriaId);
    }

    public async Task AtualizarAsync(int id, string novoTitulo, string? novaDescricao, Prioridade? novaPrioridade, string? novoSolicitanteNome, int? novaCategoriaId)
    {
        var chamado = await ObterPorIdAsync(id);
        chamado.Update(novoTitulo, novaDescricao, novaPrioridade, novoSolicitanteNome, novaCategoriaId);
        await _chamadosRepository.AtualizarAsync(chamado);
    }

    public async Task DeletarAsync(int id)
    {
        var chamado = await ObterPorIdAsync(id);
        if (chamado.Status != Status.Fechado)
        {
            throw new RegraDeNegocioException($"Não é possível deletar o chamado com Id {id} pois ele está '{chamado.Status}'. Somente chamados com status 'Fechado' podem ser removidos.");
        }
        await _chamadosRepository.DeletarAsync(id);
    }

    public async Task IniciarAtendimentoAsync(int id)
    {
        var chamado = await ObterPorIdAsync(id);
        chamado.IniciarAtendimento();
        await _chamadosRepository.AtualizarAsync(chamado);
    }

    public async Task EncerrarAtendimentoAsync(int id, string solucao)
    {
        var chamado = await ObterPorIdAsync(id);
        chamado.EncerrarAtendimento(solucao);
        await _chamadosRepository.AtualizarAsync(chamado);
    }    

    public async Task<Interacao> AdicionarInteracaoAsync(int id, string autor, string mensagem)
    {
        var chamado = await ObterPorIdAsync(id);
        chamado.AdicionarInteracao(autor, mensagem);
        await _chamadosRepository.AtualizarAsync(chamado);
        return chamado.Interacoes.Last();
    }
}