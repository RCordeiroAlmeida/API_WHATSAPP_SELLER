namespace API_WHATSAPP_SELLER.Interfaces
{
    public interface ITwilioService
    {
        Task EnviarMensagem(string telefone, string mensagem);
    }
}
