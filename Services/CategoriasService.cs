using DeskFlowAPI.Exceptions;
using DeskFlowAPI.Models.Entidades;
using DeskFlowAPI.Repositories.Interfaces;
using DeskFlowAPI.Services.Interfaces;

namespace DeskFlowAPI.Services;

public class CategoriasService : ICategoriasService
{
    private readonly ICategoriasInterface _repository;
    public CategoriasService(ICategoriasInterface repository)
    {
        _repository = repository;
    }
    
    public async Task<Categoria> ObterPorIdAsync (int id)
    {
        var categoria = await _repository.ObterPorIdAsync(id);

        if (categoria is null)
        {
            throw new NotFoundException($"Categoria com Id {id} não encontrada.");
        }
        return categoria;
    }

    public async Task <Categoria> CriarAsync(Categoria categoria)
    {
        await _repository.InserirAsync(categoria);
        return categoria;
    }

    public async Task AtualizarAsync(int id, string novoNome)
    {
        var categoria = await ObterPorIdAsync(id);

        categoria.Update(novoNome);

        await _repository.AtualizarAsync(categoria);
    }

    public async Task DeletarAsync(int id)
    {
        await ObterPorIdAsync(id);
        var existeChamadoVinculado = await _repository.ExisteChamadoVinculadoAsync(id);
        if (existeChamadoVinculado)
        {
            throw new RegraDeNegocioException($"Não é possível deletar a categoria com Id {id} pois existem chamados vinculados a ela.");
        }
        await _repository.DeletarAsync(id);
    }
    public async Task<List<Categoria>> ObterTodasAsync()
    {
        return await _repository.ObterTodasAsync();
    }
}