using API_WHATSAPP_SELLER.Models;

namespace API_WHATSAPP_SELLER.Interfaces
{
    public interface IClienteRepository
    {
        Task<int> CreateClienteAsync(Cliente newCliente);
        Task<List<Cliente>> GetClientesAsync();
        Task<Cliente> BuscarPorCpf(string cpf);
    }
}
