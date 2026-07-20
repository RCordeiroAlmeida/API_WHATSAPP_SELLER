namespace API_WHATSAPP_SELLER.Models
{
    public class Cliente
    {
        public int CLI_CODIGO { get; set; }
        public string CLI_NOME { get; set; }
        public string CLI_EMAIL { get; set; } = string.Empty;
        public string CLI_TELEFONE { get; set; } = string.Empty;
        public string CLI_CPFCNPJ { get; set; } = string.Empty;
        public DateTime CLI_CREATED_AT { get; set; } = DateTime.Now;
        public DateTime CLI_LAST_UPDATED_AT { get; set;} = DateTime.Now;
    }
}
