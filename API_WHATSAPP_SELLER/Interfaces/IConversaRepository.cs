using API_WHATSAPP_SELLER.Models;

namespace API_WHATSAPP_SELLER.Interfaces
{
    public interface IConversaRepository
    {
        Task<Conversa?> BuscaPorTelefone(string telefone);
        Task<int> Criar(Conversa conversa);
        Task<int> Atualizar(Conversa conversa);
    }
}
