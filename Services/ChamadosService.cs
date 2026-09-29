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

    public async Task AtualizarAsync(int id, string novoTitulo)
    {
        var chamado = await ObterPorIdAsync(id);
        chamado.Update(novoTitulo);
        await _chamadosRepository.ObterPorIdAsync(id);
    }

    public async Task DeletarAsync(int id)
    {
        await ObterPorIdAsync(id);
        await _chamadosRepository.DeletarAsync(id);
    }
}