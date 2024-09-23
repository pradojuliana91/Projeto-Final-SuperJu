namespace SuperJU.API.Controllers.Response
{
    public class ClienteResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string Telefone { get; set; }
        public required string CEP { get; set; }
        public required string Endereco { get; set; }
        public string? Complemento { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }
    }
}
