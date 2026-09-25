using System.ComponentModel.DataAnnotations;

namespace DeskFlowAPI.DTO;

public class CategoriaCreateDTO
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    public string Nome { get; set; }
}