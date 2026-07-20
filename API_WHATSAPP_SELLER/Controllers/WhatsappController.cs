using Microsoft.AspNetCore.Mvc;
using API_WHATSAPP_SELLER.Services;
using Microsoft.IdentityModel.Tokens;
using API_WHATSAPP_SELLER.Models;
using API_WHATSAPP_SELLER.Interfaces;

namespace API_WHATSAPP_SELLER.Controllers;

[ApiController]
[Route("api/whatsapp")]
public class WhatsappController : ControllerBase
{
    private readonly IBotService _botservice;
    private readonly ITwilioService _twilioService;


    public WhatsappController(IBotService botservice, ITwilioService twilioService)
    {
        _botservice = botservice;
        _twilioService = twilioService;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> ReceberMensagem()
    {
        var telefone = Request.Form["From"].ToString();
        var mensagem = Request.Form["Body"].ToString();

        await _botservice.ProcessarMensagem(telefone, mensagem);

        return Ok();
    }

    [HttpPost("enviar")]
    public IActionResult EnviarMensagem([FromBody] MensagemRequestModel request)
    {
        if(string.IsNullOrEmpty(request.Telefone) || string.IsNullOrEmpty(request.Mensagem))
        {
            return BadRequest("Telefone e Mensagem são obrigatórios.");
        }

        _twilioService.EnviarMensagem(request.Telefone, request.Mensagem);
        return Ok(new { status = "Mensagem enviada com sucesso" });
    }
}