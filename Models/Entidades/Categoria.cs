using System;

namespace DeskFlowAPI.Models.Entidades;

public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; private set; }
    public void Update(string nome)
    {
        Nome = nome;
    }
    public virtual ICollection<Chamado> Chamados { get; set; }
}
