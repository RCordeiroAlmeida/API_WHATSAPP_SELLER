using API_WHATSAPP_SELLER.Interfaces;
using API_WHATSAPP_SELLER.Models;

namespace API_WHATSAPP_SELLER.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        
        public ClienteService(IClienteRepository clienteRepository)
        {
           _clienteRepository = clienteRepository;
        }

        public async Task<int> CreateClienteAsync(Cliente newCliente)
        {   
            if(newCliente == null)
                throw new ArgumentNullException(nameof(newCliente));

            return await _clienteRepository.CreateClienteAsync(newCliente);
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _clienteRepository.GetClientesAsync();
        }

        public async Task<Cliente> BuscaPorCpf(string cpf)
        {
            return await _clienteRepository.BuscarPorCpf(cpf);
        }
    }
}
