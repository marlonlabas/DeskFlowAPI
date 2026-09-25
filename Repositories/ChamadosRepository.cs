using DeskFlowAPI.Models.Entidades;
using DeskFlowAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlowAPI.Repositories;

public class ChamadosRepository : IChamadosInterface
{
    private readonly AppDbContext _context;
    public ChamadosRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task CriarAsync(Chamado chamado)
    {
        await _context.Chamados.AddAsync(chamado);
        await _context.SaveChangesAsync();
    }
    public async Task<Chamado?> ObterPorIdAsync(int id)
    {
        return await _context.Chamados
            .Include(ch => ch.Categoria)
            .Include(ch => ch.Interacoes)
            .FirstOrDefaultAsync(ch => ch.Id == id);
    }
    public async Task<List<Chamado>> ObterComFiltrosAsync(Status? status, Prioridade? prioridade, int? categoriaId)
    {
        var consulta = _context.Chamados.AsQueryable();

        if(status.HasValue)
        {
            consulta = consulta.Where(ch => ch.Status == status.Value);
        }
        if(prioridade.HasValue)
        {
            consulta = consulta.Where(ch => ch.Prioridade == prioridade.Value);
        }
        if(categoriaId.HasValue)
        {
            consulta = consulta.Where(ch => ch.CategoriaId == categoriaId.Value);
        }
        return await consulta.ToListAsync();
    }
    public async Task AtualizarAsync(Chamado chamado)
    {
        _context.Chamados.Update(chamado);
        await _context.SaveChangesAsync();
    }
}