using System.ComponentModel.DataAnnotations;
using DeskFlowAPI.Models.Entidades;

namespace DeskFlowAPI.DTO;
public class ChamadoUpdateDTO
{
    [Required(ErrorMessage = "O título do chamado é obrigatório.")]
    public string Titulo { get; set; }
    public string? Descricao { get; set; }
    public Prioridade? Prioridade { get; set; }
    public string? SolicitanteNome { get; set; }
    public int? CategoriaId { get; set; }
}