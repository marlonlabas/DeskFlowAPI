namespace DeskFlowAPI.DTO;

public class MensagemDTO
{
    public string Mensagem { get; set; }

    public MensagemDTO(string mensagem)
    {
        Mensagem = mensagem;
    }
}