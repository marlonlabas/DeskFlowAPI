using DeskFlowAPI.Models.Entidades;
using DeskFlowAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlowAPI.Controllers;

[ApiController]
[Route("api/[controler]")]

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
        if (chamado is null)
        {
            return NotFound();
        }
        return Ok(chamado);
    }

    [HttpPost]
    public async Task <ActionResult<Chamado>> Criar(Chamado chamado)
    {
        var chamadoCriado = await _chamadosService.CriarAsync(chamado);
        return CreatedAtAction (nameof(ObterPorId), new {id = chamadoCriado.Id}, chamadoCriado);
    }

    [HttpPut]
    public async Task<ActionResult<Chamado>> Atualizar(Chamado chamado)
    {
        var chamadoAtualizado = await _chamadosService.AtualizarAsync(chamado);
        return Ok(chamadoAtualizado);
    }
}