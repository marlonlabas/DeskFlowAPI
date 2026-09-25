using DeskFlowAPI.DTO;
using DeskFlowAPI.Models.Entidades;
using DeskFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlowAPI.Controllers;

[ApiController]
[Route("api/categorias")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriasService _categoriaService;

    public CategoriaController(ICategoriasService service)
    {
        _categoriaService = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Categoria>>> ObterTodas()
    {
        var categorias = await _categoriaService.ObterTodasAsync();
        return Ok(categorias);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterPorIdAsync([FromRoute] int id)
    {
        Categoria categoria = await _categoriaService.ObterPorIdAsync(id);
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CategoriaCreateDTO dto)
    {
        var categoria = new Categoria();
        categoria.Update(dto.Nome);

        var criada = await _categoriaService.CriarAsync(categoria);

        return Created($"/api/categorias/{criada.Id}", criada);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _categoriaService.DeletarAsync(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] CategoriaUpdateDTO dto)
    {
        var categoriaAtualizada = dto.Nome;
        await _categoriaService.AtualizarAsync(id, categoriaAtualizada);
        return Ok(new MensagemDTO("Categoria atualizada com sucesso."));
    }
}