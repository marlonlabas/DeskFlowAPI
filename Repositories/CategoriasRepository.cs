using DeskFlowAPI.Models.Entidades;
using DeskFlowAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeskFlowAPI.Repositories;

public class CategoriaRepository : ICategoriasInterface
{
    private readonly AppDbContext _context;
    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Categoria>> ObterTodasAsync()
    {
        return await _context.Categorias.ToListAsync();
    }
    public async Task<Categoria?> ObterPorIdAsync(int id)
    {
        return await _context.Categorias.FindAsync(id);
    }
    public async Task InserirAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
    }
    public async Task AtualizarAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
    }
    public async Task DeletarAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria != null)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<bool> ExisteChamadoVinculadoAsync(int categoriaId)
    {
        return await _context.Chamados.AnyAsync(ch => ch.CategoriaId == categoriaId);
    }
}