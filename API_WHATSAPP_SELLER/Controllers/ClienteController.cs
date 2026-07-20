using API_WHATSAPP_SELLER.Interfaces;
using API_WHATSAPP_SELLER.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_WHATSAPP_SELLER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService) {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente newCliente)
        {
            var id = await _clienteService.CreateClienteAsync(newCliente);
            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.GetClientesAsync();
            return Ok(clientes);
        }

    }

    
}
