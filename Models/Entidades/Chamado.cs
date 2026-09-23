using System;

namespace DeskFlowAPI.Entidades;

public class Chamado
{
    public string Id { get; set; }

    public string Descricao { get; set; }

    public string CategoriaId { get; set; }

    public virtual Categoria Categoria { get; set; }
}
