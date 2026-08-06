using API_WHATSAPP_SELLER.Interfaces;
using API_WHATSAPP_SELLER.Models;
using API_WHATSAPP_SELLER.Models.Enum;

namespace API_WHATSAPP_SELLER.Services
{
    public class BotService : IBotService
    {
        private readonly ITwilioService _twilioService;
        private readonly IConversaRepository _conversaRepository;
        private readonly IClienteService _clienteService;

        public BotService(
            ITwilioService twilioService,
            IConversaRepository conversaRepository,
            IClienteService clienteService)
        {
            _twilioService = twilioService;
            _conversaRepository = conversaRepository;
            _clienteService = clienteService;
        }

        public async Task ProcessarMensagem(string telefone, string mensagem)
        {
            var conversa = await _conversaRepository.BuscaPorTelefone(telefone);

            if (conversa == null)
            {
                await IniciarConversa(telefone);
                return;
            }

            switch (conversa.CON_ETAPA)
            {
                case EtapaConversa.AguardandoCpf:
                    await ProcessarCpf(conversa, mensagem);
                    break;

                case EtapaConversa.AguardandoNome:
                    await ProcessarNome(conversa, mensagem);
                    break;

                case EtapaConversa.AguardandoEmail:
                    await ProcessarEmail(conversa, mensagem);
                    break;

                case EtapaConversa.AguardandoTelefone:
                    await ProcessarTelefone(conversa, mensagem);
                    break;

                case EtapaConversa.MenuCliente:
                    await ProcessarMenu(conversa, mensagem);
                    break;

                // case EtapaConversa.NovoPedido:
                //     await ProcessarNovoPedido(conversa, mensagem);
                //     break;

                // case EtapaConversa.AcompanhandoPedido:
                //     await ProcessarAcompanhamentoPedido(conversa, mensagem);
                //     break;
            }
        }

        private async Task IniciarConversa(string telefone)
        {
            var conversa = new Conversa
            {
                CON_TELEFONE = telefone,
                CON_ETAPA = EtapaConversa.AguardandoCpf
            };

            await _conversaRepository.Criar(conversa);

            await _twilioService.EnviarMensagem(
                telefone,
                "Olá! Seja bem-vindo.\n\nInforme seu CPF ou CNPJ para continuarmos.");
        }

        private async Task ProcessarCpf(Conversa conversa, string cpf)
        {
            var cliente = await _clienteService.BuscaPorCpf(cpf);

            if (cliente != null)
            {
                conversa.CON_CPF = cliente.CLI_CPFCNPJ;
                conversa.CON_ETAPA = EtapaConversa.MenuCliente;

                await _conversaRepository.Atualizar(conversa);

                await _twilioService.EnviarMensagem(
                    conversa.CON_TELEFONE,
                    $"Olá {cliente.CLI_NOME}!\n\n1 - Novo Pedido\n2 - Acompanhar Pedido");

                return;
            }

            conversa.CON_CPF = cpf;
            conversa.CON_ETAPA = EtapaConversa.AguardandoNome;

            await _conversaRepository.Atualizar(conversa);

            await _twilioService.EnviarMensagem(
                conversa.CON_TELEFONE,
                "Não encontramos seu cadastro.\n\nQual seu nome?");
        }

        private async Task ProcessarNome(Conversa conversa, string nome)
        {
            conversa.CON_NOME = nome;
            conversa.CON_ETAPA = EtapaConversa.AguardandoEmail;

            await _conversaRepository.Atualizar(conversa);

            await _twilioService.EnviarMensagem(
                conversa.CON_TELEFONE,
                "Informe seu e-mail.");
        }

        private async Task ProcessarEmail(Conversa conversa, string email)
        {
            conversa.CON_EMAIL = email;
            conversa.CON_ETAPA = EtapaConversa.AguardandoTelefone;

            await _conversaRepository.Atualizar(conversa);

            await _twilioService.EnviarMensagem(
                conversa.CON_TELEFONE,
                "Informe seu telefone.");
        }

        private async Task ProcessarTelefone(Conversa conversa, string telefone)
        {
            await _clienteService.CreateClienteAsync(new Cliente
            {
                CLI_NOME = conversa.CON_NOME,
                CLI_EMAIL = conversa.CON_EMAIL,
                CLI_TELEFONE = telefone,
                CLI_CPFCNPJ = conversa.CON_CPF
            });

            conversa.CON_ETAPA = EtapaConversa.MenuCliente;

            await _conversaRepository.Atualizar(conversa);

            await _twilioService.EnviarMensagem(
                conversa.CON_TELEFONE,
                "Cadastro realizado!\n\n1 - Novo Pedido\n2 - Acompanhar Pedido");
        }

        private async Task ProcessarMenu(Conversa conversa, string opcao)
        {
            switch (opcao)
            {
                case "1":
                    await _twilioService.EnviarMensagem(
                        conversa.CON_TELEFONE,
                        "Vamos iniciar um novo pedido.");
                    break;

                case "2":
                    await _twilioService.EnviarMensagem(
                        conversa.CON_TELEFONE,
                        "Informe o número do pedido.");
                    break;

                default:
                    await _twilioService.EnviarMensagem(
                        conversa.CON_TELEFONE,
                        "Opção inválida.\n\n1 - Novo Pedido\n2 - Acompanhar Pedido");
                    break;
            }
        }
    }
}
