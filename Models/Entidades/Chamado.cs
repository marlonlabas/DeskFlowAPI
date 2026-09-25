using DeskFlowAPI.Exceptions;

namespace DeskFlowAPI.Models.Entidades;
public class Chamado
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Prioridade Prioridade { get; set; }
    public Status Status { get; private set; } = Status.Aberto;
    public string SolicitanteNome { get; set; }
    public DateTime DataAbertura { get; set; } = DateTime.Now;
    public DateTime? DataFechamento { get; private set; }
    public string Solucao { get; private set; }
    public int CategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }
    public virtual ICollection<Interacao> Interacoes { get; set; } = new List<Interacao>();

    public void IniciarAtendimento()
    {
        if (Status != Status.Aberto)
            throw new RegraDeNegocioException("Só é possível iniciar o atendimento de um chamado que está 'Aberto'.");
        Status = Status.EmAndamento;
    }

    public void EncerrarAtendimento(string solucao)
    {
        if (Status != Status.EmAndamento)
            throw new RegraDeNegocioException("Só é possível encerrar um atendimento que esteja 'Em Andamento'.");
        Status = Status.Fechado;
        Solucao = solucao;
        DataFechamento = DateTime.Now;
    }

    public void AdicionarInteracao (string autor, string mensagem)
    {
        if (Status == Status.Fechado)
            throw new RegraDeNegocioException("Este chamado já foi encerrado.");
        
        var novaInteracao = new Interacao
        {
            Autor = autor,
            Mensagem = mensagem
        };
        Interacoes.Add(novaInteracao);
    }
}
