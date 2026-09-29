using DeskFlowAPI.DTO;
using DeskFlowAPI.Models.Entidades;
using DeskFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlowAPI.Controllers;

[ApiController]
[Route("api/chamados")]

public class ChamadosController : ControllerBase
{
    private readonly IChamadosService _chamadosService;
    public ChamadosController(IChamadosService chamadosService)
    {
        _chamadosService = chamadosService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Chamado>>> ObterComFiltros(
        [FromQuery] Status? status,
        [FromQuery] Prioridade? prioridade,
        [FromQuery] int? categoriaId)
    {
        var chamados = await _chamadosService.ObterComFiltrosAsync(status, prioridade, categoriaId);
        return Ok(chamados);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Chamado>> ObterPorId(int id)
    {
        var chamado = await _chamadosService.ObterPorIdAsync(id);
        return Ok(chamado);
    }

    [HttpPost]
    public async Task<ActionResult<Chamado>> Criar([FromBody] ChamadoCreateDTO dto)
    {
        var chamado = new Chamado
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Prioridade = dto.Prioridade,
            SolicitanteNome = dto.SolicitanteNome,
            CategoriaId = dto.CategoriaId
        };
        var chamadoCriado = await _chamadosService.CriarAsync(chamado);
        return CreatedAtAction(nameof(ObterPorId), new { id = chamadoCriado.Id }, chamadoCriado);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MensagemDTO>> Atualizar([FromRoute] int id, [FromBody] ChamadoUpdateDTO dto)
    {
        await _chamadosService.AtualizarAsync(id, dto.Titulo, dto.Descricao, dto.Prioridade, dto.SolicitanteNome, dto.CategoriaId);
        return Ok(new MensagemDTO("Chamado atualizado com sucesso."));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        await _chamadosService.DeletarAsync(id);
        return NoContent();
    }
}