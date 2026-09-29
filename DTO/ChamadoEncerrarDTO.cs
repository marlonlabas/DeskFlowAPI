using System.ComponentModel.DataAnnotations;

namespace DeskFlowAPI.DTO;

public class ChamadoEncerrarDTO
{
    [Required(ErrorMessage ="A solução do chamado é obrigatória para encerrá-lo.")]
    public string Solucao { get; set;}
}