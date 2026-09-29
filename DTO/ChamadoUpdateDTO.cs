using System.ComponentModel.DataAnnotations;

namespace DeskFlowAPI.DTO;
public class ChamadoUpdateDTO
{
    [Required(ErrorMessage = "O título do chamado é obrigatório.")]
    public string Titulo { get; set; }
}