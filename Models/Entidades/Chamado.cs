using System;
using DeskFlowAPI.Models.Entidades;

namespace DeskFlowAPI.Models.Entidades;
public class Chamado
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Prioridade Prioridade { get; set; }
    public Status Status { get; set; }
    public string SolicitanteNome { get; set; }
    public DateTime DataAbertura { get; set; } = DateTime.Now;
    public DateTime? DataFechamento { get; set; }
    public string Solucao { get; set; }
    public int CategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }
}
