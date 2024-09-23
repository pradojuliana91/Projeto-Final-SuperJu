namespace SuperJU.API.Controllers.Request
{
    public class ClienteCadstroEditarRequest
    {
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public string? CEP { get; set; }
        public string? Endereco { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
    }
}
