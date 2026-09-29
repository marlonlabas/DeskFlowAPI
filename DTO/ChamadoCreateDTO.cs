using System.ComponentModel.DataAnnotations;
using DeskFlowAPI.Models.Entidades;

namespace DeskFlowAPI.DTO;
public class ChamadoCreateDTO
{
    [Required(ErrorMessage = "O título do chamado é obrigatório.")]
    public string Titulo { get; set; }
    
    [Required(ErrorMessage = "A descrição do chamado é obrigatória.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "A prioridade do chamado é obrigatória.")]
    public Prioridade Prioridade { get; set; }

    [Required(ErrorMessage = "O nome do solicitante é obrigatório.")]
    public string SolicitanteNome { get; set; }

    [Required(ErrorMessage = "A categoria do chamado é obrigatória.")]
    public int CategoriaId { get; set; }
}