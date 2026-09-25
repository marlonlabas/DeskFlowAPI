using DeskFlowAPI.Models.Entidades;
using DeskFlowAPI.Repositories.Interfaces;
using DeskFlowAPI.Services.Interfaces;

namespace DeskFlowAPI.Services;

public class ChamadosService : IChamadosServices
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

    public async Task<Chamado?> ObterPorIdAsync(int id)
    {
        return await _chamadosRepository.ObterPorIdAsync(id);
    }

    public async Task<List<Chamado>> ObterComFiltrosAsync(Status? status, Prioridade? prioridade, int? categoriaId)
    {
        return await _chamadosRepository.ObterComFiltrosAsync(status, prioridade, categoriaId);
    }

    public async Task<Chamado> AtualizarAsync(Chamado chamado)
    {
        await _chamadosRepository.AtualizarAsync(chamado);
        return chamado;
    }
}