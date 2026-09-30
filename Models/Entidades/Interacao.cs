using System.Text.Json.Serialization;

namespace DeskFlowAPI.Models.Entidades;
public class Interacao
{
    public int Id { get; set; }
    public int ChamadoId { get; set; }
    public string Autor { get; set; }
    public string Mensagem { get; set; }
    public DateTime DataRegistro { get; set; } = DateTime.Now;

    [JsonIgnore]
    public virtual Chamado Chamado { get; set; }
}