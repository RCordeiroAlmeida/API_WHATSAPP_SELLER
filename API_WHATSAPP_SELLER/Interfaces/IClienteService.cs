using API_WHATSAPP_SELLER.Models;

namespace API_WHATSAPP_SELLER.Interfaces
{
    public interface IClienteService
    {
        Task<int> CreateClienteAsync(Cliente newCliente);
        Task<List<Cliente>> GetClientesAsync();
        Task<Cliente> BuscaPorCpf(string cpf);
    }
}
