using System.ComponentModel.DataAnnotations;

namespace DeskFlowAPI.DTO;

public class InteracaoCreateDTO
{
    [Required(ErrorMessage ="O autor da interação é obrigatório.")]
    public string Autor { get; set; }

    [Required(ErrorMessage ="A mensagem de interação é obrigatória.")]
    public string Mensagem { get; set; }
}