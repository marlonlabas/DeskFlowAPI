using System;

namespace DeskFlowAPI.Entidades;

public class Categoria
{
    public string Id { get; set; }

    public string Nome { get; set; }

    public virtual ICollection<Chamado> Chamados { get; set; }
}
